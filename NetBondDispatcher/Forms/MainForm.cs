using System.Drawing.Drawing2D;
using System.Net;
using NetBondDispatcher.Models;
using NetBondDispatcher.Services;

namespace NetBondDispatcher.Forms;

public partial class MainForm : Form
{
    private readonly NetworkMonitor _networkMonitor = new();
    private readonly Socks5ProxyServer _proxyServer = new();
    private readonly ChunkDownloader _downloader = new();
    private readonly SettingsManager _settingsManager = new();
    private readonly UpdateService _updateService = new();

    private AppConfig _config = new();
    private bool _isExplicitExit = false;

    // Styling colors for sidebar
    private readonly Color _navActiveColor = Color.FromArgb(37, 99, 235);
    private readonly Color _navInactiveColor = Color.Transparent;
    private readonly Color _navActiveText = Color.White;
    private readonly Color _navInactiveText = Color.FromArgb(160, 170, 190);

    public MainForm()
    {
        InitializeComponent();
        SetupFormIcons();
        SetupServiceCallbacks();
    }

    private void SetupFormIcons()
    {
        try
        {
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "app.ico");
            if (File.Exists(iconPath))
            {
                var appIcon = new Icon(iconPath);
                this.Icon = appIcon;
                notifyIcon.Icon = appIcon;
                return;
            }

            // Fallback for single-file published packages: extract embedded application icon
            string? exePath = Environment.ProcessPath ?? Application.ExecutablePath;
            if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
            {
                var extracted = Icon.ExtractAssociatedIcon(exePath);
                if (extracted != null)
                {
                    this.Icon = extracted;
                    notifyIcon.Icon = extracted;
                    return;
                }
            }
        }
        catch
        {
            // Fallback to system icon if load fails
        }

        this.Icon = SystemIcons.Application;
        notifyIcon.Icon = SystemIcons.Application;
    }

    private void SetupServiceCallbacks()
    {
        // 1. SOCKS5 Server Logging & Metrics
        _proxyServer.OnLog += msg =>
        {
            SafeInvoke(() => AppendLog(msg));
        };

        _proxyServer.OnActiveConnectionsChanged += count =>
        {
            SafeInvoke(() =>
            {
                lblDashProxySessions.Text = $"Active Socket Sessions: {count}";
                lblProxyActiveSessionsVal.Text = count.ToString();
            });
        };

        _proxyServer.OnSessionDistributionChanged += sessions =>
        {
            _networkMonitor.UpdateActiveSessions(sessions);
        };

        // 2. Network Monitor Metrics Update Callback
        _networkMonitor.OnMetricsUpdated += (adapters, totalDownBps, totalUpBps) =>
        {
            SafeInvoke(() =>
            {
                // Update Dashboard metric cards
                lblCardDownloadVal.Text = NetworkAdapterInfo.FormatBytesPerSec(totalDownBps);
                lblCardUploadVal.Text = NetworkAdapterInfo.FormatBytesPerSec(totalUpBps);
                lblCardAdaptersVal.Text = adapters.Count(a => a.IsIncludedInPool).ToString();

                // Update DataGridView live speed cells
                UpdateAdapterGridMetrics(adapters);
            });
        };

        // 3. Chunk Downloader Callbacks
        _downloader.OnLog += msg =>
        {
            SafeInvoke(() =>
            {
                lblOverallDownloadProgress.Text = msg;
                AppendLog(msg);
            });
        };

        _downloader.OnOverallProgress += (downloaded, total, speedMBps) =>
        {
            SafeInvoke(() =>
            {
                if (total > 0)
                {
                    int pct = (int)Math.Clamp((downloaded * 100) / total, 0, 100);
                    pbDownloader.Value = pct;
                    lblOverallDownloadProgress.Text = $"{downloaded / (1024.0 * 1024.0):F2} MB / {total / (1024.0 * 1024.0):F2} MB ({pct}%)";
                }
                else
                {
                    lblOverallDownloadProgress.Text = $"Downloaded: {downloaded / (1024.0 * 1024.0):F2} MB";
                }

                lblOverallDownloadSpeed.Text = $"Speed: {speedMBps:F2} MB/s ({(speedMBps * 8.0):F1} Mbps)";
            });
        };

        _downloader.OnChunkProgress += progress =>
        {
            SafeInvoke(() => UpdateChunkListItem(progress));
        };

        _downloader.OnCompleted += (success, error) =>
        {
            SafeInvoke(() =>
            {
                btnStartDownload.Enabled = true;
                btnCancelDownload.Enabled = false;

                if (success)
                {
                    pbDownloader.Value = 100;
                    lblOverallDownloadProgress.Text = "Download completed successfully!";
                    AppendLog("[Downloader] Download completed successfully.");
                    MessageBox.Show(this, "Download finished successfully!", "NetBondDispatcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblOverallDownloadProgress.Text = $"Download halted: {error}";
                    if (!string.IsNullOrEmpty(error) && error != "Download canceled.")
                    {
                        MessageBox.Show(this, $"Download error: {error}", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            });
        };
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // 1. Load Settings
        _config = _settingsManager.LoadSettings();
        ApplySettingsToUI();

        // 2. Set default download destination
        string defaultDownloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        txtDestPath.Text = Path.Combine(defaultDownloads, "");
        txtDownloadUrl.Text = "";

        // 3. Scan Network Adapters
        RefreshNetworkAdapters();

        // 4. Start 1-second metric calculation timer
        timerSpeedCalculation.Start();

        AppendLog("[System] NetBond Dispatcher v1.0 initialized.");

        // 5. Auto-start proxy if configured
        if (_config.AutoStartProxy)
        {
            _ = StartProxyAsync();
        }

        // 6. Check if initial minimize to tray is configured
        if (_config.StartMinimizedToTray)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }
    }

    #region Sidebar Navigation

    private void SetActiveNav(Button activeButton, int tabIndex)
    {
        Button[] navButtons = { btnNavDashboard, btnNavAdapters, btnNavProxy, btnNavDownloader, btnNavSettings };
        foreach (var btn in navButtons)
        {
            if (btn == activeButton)
            {
                btn.BackColor = _navActiveColor;
                btn.ForeColor = _navActiveText;
            }
            else
            {
                btn.BackColor = _navInactiveColor;
                btn.ForeColor = _navInactiveText;
            }
        }

        tabControlViews.SelectedIndex = tabIndex;
    }

    private void btnNavDashboard_Click(object sender, EventArgs e)
    {
        SetActiveNav(btnNavDashboard, 0);
    }

    private void btnNavAdapters_Click(object sender, EventArgs e)
    {
        SetActiveNav(btnNavAdapters, 1);
    }

    private void btnNavProxy_Click(object sender, EventArgs e)
    {
        SetActiveNav(btnNavProxy, 2);
    }

    private void btnNavDownloader_Click(object sender, EventArgs e)
    {
        SetActiveNav(btnNavDownloader, 3);
    }

    private void btnNavSettings_Click(object sender, EventArgs e)
    {
        SetActiveNav(btnNavSettings, 4);
    }

    #endregion

    #region View 1: Dashboard & View 2: Adapters Logic

    private void RefreshNetworkAdapters()
    {
        _networkMonitor.ScanAdapters();
        PopulateAdaptersGrid();
        SyncProxyPool();
    }

    private void PopulateAdaptersGrid()
    {
        var adapters = _networkMonitor.GetAdapters();
        dgvAdapters.Rows.Clear();

        foreach (var adapter in adapters)
        {
            int rowIndex = dgvAdapters.Rows.Add(
                adapter.IsIncludedInPool,
                adapter.Name,
                adapter.IpAddress.ToString(),
                adapter.InterfaceType.ToString(),
                adapter.Status.ToString(),
                adapter.FormattedDownloadSpeed,
                adapter.FormattedUploadSpeed,
                adapter.ActiveSessionCount.ToString());

            dgvAdapters.Rows[rowIndex].Tag = adapter;
        }

        lblCardAdaptersVal.Text = adapters.Count(a => a.IsIncludedInPool).ToString();
    }

    private void UpdateAdapterGridMetrics(List<NetworkAdapterInfo> adapters)
    {
        foreach (DataGridViewRow row in dgvAdapters.Rows)
        {
            if (row.Tag is NetworkAdapterInfo adapter)
            {
                row.Cells[5].Value = adapter.FormattedDownloadSpeed;
                row.Cells[6].Value = adapter.FormattedUploadSpeed;
                row.Cells[7].Value = adapter.ActiveSessionCount.ToString();
            }
        }
    }

    private void SyncProxyPool()
    {
        var activeBonded = _networkMonitor.GetActiveBondedAdapters();
        _proxyServer.UpdateAdapters(activeBonded);

        lblProxyRoutingNotice.Text = $"Bonding Pool Status: {activeBonded.Count} adapter(s) active.\r\n\r\n" +
            string.Join("\r\n", activeBonded.Select(a => $"• [{a.IpAddress}] {a.Name} ({a.InterfaceType})")) +
            $"\r\n\r\nConfigure client applications to SOCKS5 host 127.0.0.1:{nudProxyPort.Value}.";
    }

    private void btnRefreshAdapters_Click(object sender, EventArgs e)
    {
        RefreshNetworkAdapters();
        AppendLog("[Network] Network adapters refreshed.");
    }

    private void btnSelectAllAdapters_Click(object sender, EventArgs e)
    {
        bool anyUnchecked = false;
        foreach (DataGridViewRow row in dgvAdapters.Rows)
        {
            if (row.Cells[0].Value is bool isChecked && !isChecked)
            {
                anyUnchecked = true;
                break;
            }
        }

        bool targetState = anyUnchecked;
        foreach (DataGridViewRow row in dgvAdapters.Rows)
        {
            row.Cells[0].Value = targetState;
            if (row.Tag is NetworkAdapterInfo adapter)
            {
                adapter.IsIncludedInPool = targetState;
                _networkMonitor.SetAdapterIncluded(adapter.Id, adapter.IpAddress.ToString(), targetState);
            }
        }

        SyncProxyPool();
    }

    private void dgvAdapters_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dgvAdapters.IsCurrentCellDirty)
        {
            dgvAdapters.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private void dgvAdapters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == 0)
        {
            var row = dgvAdapters.Rows[e.RowIndex];
            bool isIncluded = row.Cells[0].Value as bool? ?? false;
            if (row.Tag is NetworkAdapterInfo adapter)
            {
                adapter.IsIncludedInPool = isIncluded;
                _networkMonitor.SetAdapterIncluded(adapter.Id, adapter.IpAddress.ToString(), isIncluded);
                SyncProxyPool();
                lblCardAdaptersVal.Text = _networkMonitor.GetActiveBondedAdapters().Count.ToString();
            }
        }
    }

    private void timerSpeedCalculation_Tick(object sender, EventArgs e)
    {
        _networkMonitor.TickSpeedCalculation();
    }

    #endregion

    #region View 3: SOCKS5 Proxy Logic

    private async void btnToggleProxy_Click(object sender, EventArgs e)
    {
        if (_proxyServer.IsRunning)
        {
            StopProxy();
        }
        else
        {
            await StartProxyAsync();
        }
    }

    private async Task StartProxyAsync()
    {
        var activeAdapters = _networkMonitor.GetActiveBondedAdapters();
        if (activeAdapters.Count == 0)
        {
            MessageBox.Show(this, "Please enable at least one network adapter in the Network Adapters view.",
                "No Adapters Enabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int port = (int)nudProxyPort.Value;
        var strategy = cmbProxyStrategy.SelectedIndex == 1
            ? ProxyBindingStrategy.LeastConnections
            : ProxyBindingStrategy.RoundRobin;

        try
        {
            await _proxyServer.StartAsync(port, activeAdapters, strategy);

            // Update UI across views
            string statusRunning = $"Running on 127.0.0.1:{port}";
            lblDashProxyEngineState.Text = $"Engine: {statusRunning}";
            lblDashProxyEngineState.ForeColor = Color.FromArgb(16, 185, 129); // Green
            btnDashToggleProxy.Text = "Stop SOCKS5 Proxy";
            btnDashToggleProxy.BackColor = Color.FromArgb(239, 68, 68); // Red

            lblProxyStateStatus.Text = $"Status: {statusRunning}";
            lblProxyStateStatus.ForeColor = Color.FromArgb(16, 185, 129);
            btnProxyToggle.Text = "Stop Proxy";
            btnProxyToggle.BackColor = Color.FromArgb(239, 68, 68);
            nudProxyPort.Enabled = false;

            menuItemToggleProxy.Text = "Stop Proxy";
            AppendLog($"[SOCKS5] Engine started on 127.0.0.1:{port} ({strategy})");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to start SOCKS5 proxy: {ex.Message}", "Proxy Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void StopProxy()
    {
        _proxyServer.Stop();

        lblDashProxyEngineState.Text = "Engine: Stopped";
        lblDashProxyEngineState.ForeColor = Color.FromArgb(239, 68, 68);
        btnDashToggleProxy.Text = "Toggle SOCKS5 Proxy";
        btnDashToggleProxy.BackColor = Color.FromArgb(37, 99, 235);

        lblProxyStateStatus.Text = "Status: STOPPED";
        lblProxyStateStatus.ForeColor = Color.FromArgb(239, 68, 68);
        btnProxyToggle.Text = "Start Proxy";
        btnProxyToggle.BackColor = Color.FromArgb(37, 99, 235);
        nudProxyPort.Enabled = true;

        menuItemToggleProxy.Text = "Start Proxy";
        AppendLog("[SOCKS5] Engine stopped.");
    }

    private void cmbProxyStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        _proxyServer.Strategy = cmbProxyStrategy.SelectedIndex == 1
            ? ProxyBindingStrategy.LeastConnections
            : ProxyBindingStrategy.RoundRobin;
    }

    #endregion

    #region View 4: Parallel Downloader Logic

    private void btnBrowseDest_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            Title = "Save Download As",
            FileName = Path.GetFileName(txtDestPath.Text),
            InitialDirectory = Path.GetDirectoryName(txtDestPath.Text),
            Filter = "All Files (*.*)|*.*"
        };

        if (sfd.ShowDialog(this) == DialogResult.OK)
        {
            txtDestPath.Text = sfd.FileName;
        }
    }

    private async void btnStartDownload_Click(object sender, EventArgs e)
    {
        string url = txtDownloadUrl.Text.Trim();
        string dest = txtDestPath.Text.Trim();

        if (string.IsNullOrEmpty(url) || !Uri.TryCreate(url, UriKind.Absolute, out _))
        {
            MessageBox.Show(this, "Please specify a valid HTTP or HTTPS download URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Auto-resolve and extract file name if user selected a directory or extensionless path
        try
        {
            string fileName = Path.GetFileName(new Uri(url).LocalPath);
            if (string.IsNullOrWhiteSpace(fileName)) fileName = "downloaded_file.bin";

            if (string.IsNullOrEmpty(dest))
            {
                dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);
            }
            else if (Directory.Exists(dest) || !Path.HasExtension(dest) || dest.EndsWith("\\") || dest.EndsWith("/"))
            {
                dest = Path.Combine(dest, fileName);
            }
            txtDestPath.Text = dest;
        }
        catch
        {
            if (string.IsNullOrEmpty(dest))
            {
                MessageBox.Show(this, "Please choose a valid destination file path.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        var activeAdapters = _networkMonitor.GetActiveBondedAdapters();
        if (activeAdapters.Count == 0)
        {
            MessageBox.Show(this, "No active network adapters are enabled in the Bonding Pool.", "No Adapters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int chunkCount = (int)nudChunkCount.Value;

        btnStartDownload.Enabled = false;
        btnCancelDownload.Enabled = true;
        pbDownloader.Value = 0;
        lblOverallDownloadProgress.Text = "Initializing parallel download...";
        lblOverallDownloadSpeed.Text = "Speed: 0.00 MB/s";

        // Setup ListView items for each chunk
        lvChunkStats.Items.Clear();
        for (int i = 0; i < chunkCount; i++)
        {
            var assignedAdapter = activeAdapters[i % activeAdapters.Count];
            var lvi = new ListViewItem($"Chunk #{i + 1}");
            lvi.SubItems.Add(assignedAdapter.Name);
            lvi.SubItems.Add(assignedAdapter.IpAddress.ToString());
            lvi.SubItems.Add("0.0 B/s");
            lvi.SubItems.Add("Connecting...");
            lvi.Tag = i;
            lvChunkStats.Items.Add(lvi);
        }

        await _downloader.StartDownloadAsync(url, dest, activeAdapters, chunkCount);
    }

    private void btnCancelDownload_Click(object sender, EventArgs e)
    {
        _downloader.Cancel();
        btnCancelDownload.Enabled = false;
    }

    private void UpdateChunkListItem(DownloadProgressInfo progress)
    {
        foreach (ListViewItem item in lvChunkStats.Items)
        {
            if (item.Tag is int idx && idx == progress.ChunkIndex)
            {
                if (!string.IsNullOrEmpty(progress.ErrorMessage))
                {
                    item.SubItems[3].Text = "0.0 B/s";
                    item.SubItems[4].Text = "Failed (Check Log)";
                    item.ForeColor = Color.FromArgb(239, 68, 68);
                }
                else
                {
                    item.SubItems[3].Text = $"{progress.SpeedMBps:F2} MB/s";
                    item.SubItems[4].Text = progress.IsCompleted ? "Completed" : "Downloading";
                }
                break;
            }
        }
    }

    #endregion

    #region View 5: Settings & Updates Logic

    private void ApplySettingsToUI()
    {
        chkStartWithWindows.Checked = _config.StartWithWindows;
        chkStartMinimized.Checked = _config.StartMinimizedToTray;
        chkMinimizeOnClose.Checked = _config.MinimizeOnClose;
        chkAutoStartProxy.Checked = _config.AutoStartProxy;
        nudProxyPort.Value = Math.Clamp(_config.ProxyPort, 1, 65535);
        cmbProxyStrategy.SelectedIndex = _config.ProxyStrategy == "LeastConnections" ? 1 : 0;
        nudChunkCount.Value = Math.Clamp(_config.StreamsPerAdapter * 2, 1, 32);
        txtRepoUrl.Text = _config.GitHubRepoUrl;
    }

    private void btnSaveSettings_Click(object sender, EventArgs e)
    {
        _config.StartWithWindows = chkStartWithWindows.Checked;
        _config.StartMinimizedToTray = chkStartMinimized.Checked;
        _config.MinimizeOnClose = chkMinimizeOnClose.Checked;
        _config.AutoStartProxy = chkAutoStartProxy.Checked;
        _config.ProxyPort = (int)nudProxyPort.Value;
        _config.ProxyStrategy = cmbProxyStrategy.SelectedIndex == 1 ? "LeastConnections" : "RoundRobin";
        _config.GitHubRepoUrl = txtRepoUrl.Text.Trim();

        bool saved = _settingsManager.SaveSettings(_config);
        if (saved)
        {
            AppendLog("[Settings] Preferences saved to %AppData%\\NetBondDispatcher\\config.json.");
            MessageBox.Show(this, "Settings saved successfully.", "NetBondDispatcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show(this, "Failed to persist settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnCheckUpdates_Click(object sender, EventArgs e)
    {
        btnCheckUpdates.Enabled = false;
        lblUpdateStatus.Text = "Checking for updates via GitHub Releases...";
        lblUpdateStatus.ForeColor = Color.FromArgb(37, 99, 235);

        var (hasUpdate, newVersion, message) = await _updateService.CheckForUpdatesAsync(txtRepoUrl.Text);

        lblUpdateStatus.Text = message;
        lblUpdateStatus.ForeColor = hasUpdate ? Color.FromArgb(16, 185, 129) : Color.FromArgb(100, 116, 139);
        AppendLog($"[Velopack] {message}");

        if (hasUpdate)
        {
            var dr = MessageBox.Show(this,
                $"Version {newVersion} is available. Download and install now?",
                "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                lblUpdateStatus.Text = "Downloading update package...";
                bool applied = await _updateService.DownloadAndApplyUpdateAsync(txtRepoUrl.Text, pct =>
                {
                    SafeInvoke(() => lblUpdateStatus.Text = $"Downloading update: {pct}%");
                });

                if (!applied)
                {
                    MessageBox.Show(this, "Could not apply update. Ensure app was installed via Velopack.", "Update Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        btnCheckUpdates.Enabled = true;
    }

    #endregion

    #region Console Logging & System Tray

    private void AppendLog(string message)
    {
        string timestamped = $"[{DateTime.Now:HH:mm:ss}] {message}";
        if (txtActivityLog.TextLength > 40000)
        {
            txtActivityLog.Text = txtActivityLog.Text.Substring(15000);
        }
        txtActivityLog.AppendText(timestamped + Environment.NewLine);
    }

    private void btnClearLogs_Click(object sender, EventArgs e)
    {
        txtActivityLog.Clear();
    }

    private void notifyIcon_DoubleClick(object sender, EventArgs e)
    {
        RestoreWindow();
    }

    private void menuItemShow_Click(object sender, EventArgs e)
    {
        RestoreWindow();
    }

    private async void menuItemToggleProxy_Click(object sender, EventArgs e)
    {
        if (_proxyServer.IsRunning)
            StopProxy();
        else
            await StartProxyAsync();
    }

    private void menuItemExit_Click(object sender, EventArgs e)
    {
        _isExplicitExit = true;
        _proxyServer.Stop();
        _downloader.Cancel();
        notifyIcon.Visible = false;
        Application.Exit();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!_isExplicitExit && chkMinimizeOnClose.Checked && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            this.Hide();
            notifyIcon.ShowBalloonTip(1500, "NetBondDispatcher",
                "NetBondDispatcher minimized to system notification tray.", ToolTipIcon.Info);
        }
        else
        {
            timerSpeedCalculation.Stop();
            _proxyServer.Stop();
            _downloader.Cancel();
            notifyIcon.Visible = false;
        }
    }

    private void RestoreWindow()
    {
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.BringToFront();
        this.Activate();
    }

    private void SafeInvoke(Action action)
    {
        if (this.IsDisposed || !this.IsHandleCreated)
            return;

        if (this.InvokeRequired)
        {
            try
            {
                this.BeginInvoke(action);
            }
            catch (ObjectDisposedException)
            {
            }
        }
        else
        {
            action();
        }
    }

    #endregion
}
