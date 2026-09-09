namespace NetBondDispatcher.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        panelSidebar = new Panel();
        btnNavSettings = new Button();
        btnNavDownloader = new Button();
        btnNavProxy = new Button();
        btnNavAdapters = new Button();
        btnNavDashboard = new Button();
        panelBrand = new Panel();
        lblBrandSubtitle = new Label();
        lblBrandTitle = new Label();
        panelBottomFooter = new Panel();
        btnClearLogs = new Button();
        lblFooterTitle = new Label();
        txtActivityLog = new TextBox();
        panelMainContent = new Panel();
        tabControlViews = new TabControl();
        tabDashboard = new TabPage();
        grpProxyQuickCard = new GroupBox();
        lblDashProxySessions = new Label();
        btnDashRefreshAdapters = new Button();
        btnDashToggleProxy = new Button();
        lblDashProxyEngineState = new Label();
        panelCardAdapters = new Panel();
        lblCardAdaptersSub = new Label();
        lblCardAdaptersVal = new Label();
        lblCardAdaptersTitle = new Label();
        panelCardUpload = new Panel();
        lblCardUploadSub = new Label();
        lblCardUploadVal = new Label();
        lblCardUploadTitle = new Label();
        panelCardDownload = new Panel();
        lblCardDownloadSub = new Label();
        lblCardDownloadVal = new Label();
        lblCardDownloadTitle = new Label();
        tabAdapters = new TabPage();
        dgvAdapters = new DataGridView();
        colActive = new DataGridViewCheckBoxColumn();
        colName = new DataGridViewTextBoxColumn();
        colIp = new DataGridViewTextBoxColumn();
        colType = new DataGridViewTextBoxColumn();
        colStatus = new DataGridViewTextBoxColumn();
        colDownSpeed = new DataGridViewTextBoxColumn();
        colUpSpeed = new DataGridViewTextBoxColumn();
        colSessions = new DataGridViewTextBoxColumn();
        panelAdaptersTop = new Panel();
        btnSelectAllAdapters = new Button();
        btnRefreshAdaptersGrid = new Button();
        lblAdaptersViewTitle = new Label();
        tabProxy = new TabPage();
        grpProxyActiveAdapters = new GroupBox();
        lblProxyRoutingNotice = new Label();
        grpProxyConfig = new GroupBox();
        lblProxyActiveSessionsVal = new Label();
        lblProxyActiveSessions = new Label();
        btnProxyToggle = new Button();
        lblProxyStateStatus = new Label();
        cmbProxyStrategy = new ComboBox();
        lblProxyStrategy = new Label();
        nudProxyPort = new NumericUpDown();
        lblProxyPort = new Label();
        tabDownloader = new TabPage();
        grpDownloaderChunks = new GroupBox();
        lvChunkStats = new ListView();
        colChunkId = new ColumnHeader();
        colChunkAdapter = new ColumnHeader();
        colChunkIp = new ColumnHeader();
        colChunkSpeed = new ColumnHeader();
        colChunkStatus = new ColumnHeader();
        lblOverallDownloadSpeed = new Label();
        lblOverallDownloadProgress = new Label();
        pbDownloader = new ProgressBar();
        grpDownloaderConfig = new GroupBox();
        btnCancelDownload = new Button();
        btnStartDownload = new Button();
        nudChunkCount = new NumericUpDown();
        lblChunkCount = new Label();
        btnBrowseDest = new Button();
        txtDestPath = new TextBox();
        lblDestPath = new Label();
        txtDownloadUrl = new TextBox();
        lblDownloadUrl = new Label();
        tabSettings = new TabPage();
        grpUpdates = new GroupBox();
        lblUpdateStatus = new Label();
        btnCheckUpdates = new Button();
        txtRepoUrl = new TextBox();
        lblRepoUrl = new Label();
        grpGeneralSettings = new GroupBox();
        btnSaveSettings = new Button();
        chkAutoStartProxy = new CheckBox();
        chkMinimizeOnClose = new CheckBox();
        chkStartMinimized = new CheckBox();
        chkStartWithWindows = new CheckBox();
        notifyIcon = new NotifyIcon(components);
        trayMenu = new ContextMenuStrip(components);
        menuItemShow = new ToolStripMenuItem();
        menuSeparator1 = new ToolStripSeparator();
        menuItemToggleProxy = new ToolStripMenuItem();
        menuSeparator2 = new ToolStripSeparator();
        menuItemExit = new ToolStripMenuItem();
        timerSpeedCalculation = new System.Windows.Forms.Timer(components);
        panelSidebar.SuspendLayout();
        panelBrand.SuspendLayout();
        panelBottomFooter.SuspendLayout();
        panelMainContent.SuspendLayout();
        tabControlViews.SuspendLayout();
        tabDashboard.SuspendLayout();
        grpProxyQuickCard.SuspendLayout();
        panelCardAdapters.SuspendLayout();
        panelCardUpload.SuspendLayout();
        panelCardDownload.SuspendLayout();
        tabAdapters.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAdapters).BeginInit();
        panelAdaptersTop.SuspendLayout();
        tabProxy.SuspendLayout();
        grpProxyActiveAdapters.SuspendLayout();
        grpProxyConfig.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudProxyPort).BeginInit();
        tabDownloader.SuspendLayout();
        grpDownloaderChunks.SuspendLayout();
        grpDownloaderConfig.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudChunkCount).BeginInit();
        tabSettings.SuspendLayout();
        grpUpdates.SuspendLayout();
        grpGeneralSettings.SuspendLayout();
        trayMenu.SuspendLayout();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.FromArgb(24, 28, 37);
        panelSidebar.Controls.Add(btnNavSettings);
        panelSidebar.Controls.Add(btnNavDownloader);
        panelSidebar.Controls.Add(btnNavProxy);
        panelSidebar.Controls.Add(btnNavAdapters);
        panelSidebar.Controls.Add(btnNavDashboard);
        panelSidebar.Controls.Add(panelBrand);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(220, 720);
        panelSidebar.TabIndex = 0;
        // 
        // btnNavSettings
        // 
        btnNavSettings.Cursor = Cursors.Hand;
        btnNavSettings.Dock = DockStyle.Top;
        btnNavSettings.FlatAppearance.BorderSize = 0;
        btnNavSettings.FlatStyle = FlatStyle.Flat;
        btnNavSettings.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        btnNavSettings.ForeColor = Color.FromArgb(160, 170, 190);
        btnNavSettings.Location = new Point(0, 275);
        btnNavSettings.Name = "btnNavSettings";
        btnNavSettings.Padding = new Padding(20, 0, 0, 0);
        btnNavSettings.Size = new Size(220, 50);
        btnNavSettings.TabIndex = 5;
        btnNavSettings.Text = "⚙  Settings && Updates";
        btnNavSettings.TextAlign = ContentAlignment.MiddleLeft;
        btnNavSettings.UseVisualStyleBackColor = true;
        btnNavSettings.Click += btnNavSettings_Click;
        // 
        // btnNavDownloader
        // 
        btnNavDownloader.Cursor = Cursors.Hand;
        btnNavDownloader.Dock = DockStyle.Top;
        btnNavDownloader.FlatAppearance.BorderSize = 0;
        btnNavDownloader.FlatStyle = FlatStyle.Flat;
        btnNavDownloader.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        btnNavDownloader.ForeColor = Color.FromArgb(160, 170, 190);
        btnNavDownloader.Location = new Point(0, 225);
        btnNavDownloader.Name = "btnNavDownloader";
        btnNavDownloader.Padding = new Padding(20, 0, 0, 0);
        btnNavDownloader.Size = new Size(220, 50);
        btnNavDownloader.TabIndex = 4;
        btnNavDownloader.Text = "⬇  Parallel Downloader";
        btnNavDownloader.TextAlign = ContentAlignment.MiddleLeft;
        btnNavDownloader.UseVisualStyleBackColor = true;
        btnNavDownloader.Click += btnNavDownloader_Click;
        // 
        // btnNavProxy
        // 
        btnNavProxy.Cursor = Cursors.Hand;
        btnNavProxy.Dock = DockStyle.Top;
        btnNavProxy.FlatAppearance.BorderSize = 0;
        btnNavProxy.FlatStyle = FlatStyle.Flat;
        btnNavProxy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        btnNavProxy.ForeColor = Color.FromArgb(160, 170, 190);
        btnNavProxy.Location = new Point(0, 175);
        btnNavProxy.Name = "btnNavProxy";
        btnNavProxy.Padding = new Padding(20, 0, 0, 0);
        btnNavProxy.Size = new Size(220, 50);
        btnNavProxy.TabIndex = 3;
        btnNavProxy.Text = "⇄  SOCKS5 Proxy";
        btnNavProxy.TextAlign = ContentAlignment.MiddleLeft;
        btnNavProxy.UseVisualStyleBackColor = true;
        btnNavProxy.Click += btnNavProxy_Click;
        // 
        // btnNavAdapters
        // 
        btnNavAdapters.Cursor = Cursors.Hand;
        btnNavAdapters.Dock = DockStyle.Top;
        btnNavAdapters.FlatAppearance.BorderSize = 0;
        btnNavAdapters.FlatStyle = FlatStyle.Flat;
        btnNavAdapters.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        btnNavAdapters.ForeColor = Color.FromArgb(160, 170, 190);
        btnNavAdapters.Location = new Point(0, 125);
        btnNavAdapters.Name = "btnNavAdapters";
        btnNavAdapters.Padding = new Padding(20, 0, 0, 0);
        btnNavAdapters.Size = new Size(220, 50);
        btnNavAdapters.TabIndex = 2;
        btnNavAdapters.Text = "⛫  Network Adapters";
        btnNavAdapters.TextAlign = ContentAlignment.MiddleLeft;
        btnNavAdapters.UseVisualStyleBackColor = true;
        btnNavAdapters.Click += btnNavAdapters_Click;
        // 
        // btnNavDashboard
        // 
        btnNavDashboard.BackColor = Color.FromArgb(37, 99, 235);
        btnNavDashboard.Cursor = Cursors.Hand;
        btnNavDashboard.Dock = DockStyle.Top;
        btnNavDashboard.FlatAppearance.BorderSize = 0;
        btnNavDashboard.FlatStyle = FlatStyle.Flat;
        btnNavDashboard.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        btnNavDashboard.ForeColor = Color.White;
        btnNavDashboard.Location = new Point(0, 75);
        btnNavDashboard.Name = "btnNavDashboard";
        btnNavDashboard.Padding = new Padding(20, 0, 0, 0);
        btnNavDashboard.Size = new Size(220, 50);
        btnNavDashboard.TabIndex = 1;
        btnNavDashboard.Text = "▦  Dashboard";
        btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
        btnNavDashboard.UseVisualStyleBackColor = false;
        btnNavDashboard.Click += btnNavDashboard_Click;
        // 
        // panelBrand
        // 
        panelBrand.BackColor = Color.FromArgb(17, 20, 28);
        panelBrand.Controls.Add(lblBrandSubtitle);
        panelBrand.Controls.Add(lblBrandTitle);
        panelBrand.Dock = DockStyle.Top;
        panelBrand.Location = new Point(0, 0);
        panelBrand.Name = "panelBrand";
        panelBrand.Size = new Size(220, 75);
        panelBrand.TabIndex = 0;
        // 
        // lblBrandSubtitle
        // 
        lblBrandSubtitle.AutoSize = true;
        lblBrandSubtitle.Font = new Font("Segoe UI", 8F);
        lblBrandSubtitle.ForeColor = Color.FromArgb(120, 130, 150);
        lblBrandSubtitle.Location = new Point(18, 45);
        lblBrandSubtitle.Name = "lblBrandSubtitle";
        lblBrandSubtitle.Size = new Size(141, 13);
        lblBrandSubtitle.TabIndex = 1;
        lblBrandSubtitle.Text = "Multi-Adapter Aggregator";
        // 
        // lblBrandTitle
        // 
        lblBrandTitle.AutoSize = true;
        lblBrandTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblBrandTitle.ForeColor = Color.White;
        lblBrandTitle.Location = new Point(17, 20);
        lblBrandTitle.Name = "lblBrandTitle";
        lblBrandTitle.Size = new Size(164, 21);
        lblBrandTitle.TabIndex = 0;
        lblBrandTitle.Text = "NetBond Dispatcher";
        // 
        // panelBottomFooter
        // 
        panelBottomFooter.BackColor = Color.FromArgb(24, 24, 27);
        panelBottomFooter.Controls.Add(btnClearLogs);
        panelBottomFooter.Controls.Add(lblFooterTitle);
        panelBottomFooter.Controls.Add(txtActivityLog);
        panelBottomFooter.Dock = DockStyle.Bottom;
        panelBottomFooter.Location = new Point(220, 545);
        panelBottomFooter.Name = "panelBottomFooter";
        panelBottomFooter.Padding = new Padding(12, 8, 12, 10);
        panelBottomFooter.Size = new Size(784, 175);
        panelBottomFooter.TabIndex = 1;
        // 
        // btnClearLogs
        // 
        btnClearLogs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClearLogs.BackColor = Color.FromArgb(39, 39, 42);
        btnClearLogs.FlatAppearance.BorderSize = 0;
        btnClearLogs.FlatStyle = FlatStyle.Flat;
        btnClearLogs.Font = new Font("Segoe UI", 8F);
        btnClearLogs.ForeColor = Color.FromArgb(212, 212, 216);
        btnClearLogs.Location = new Point(702, 6);
        btnClearLogs.Name = "btnClearLogs";
        btnClearLogs.Size = new Size(70, 22);
        btnClearLogs.TabIndex = 2;
        btnClearLogs.Text = "Clear Logs";
        btnClearLogs.UseVisualStyleBackColor = false;
        btnClearLogs.Click += btnClearLogs_Click;
        // 
        // lblFooterTitle
        // 
        lblFooterTitle.AutoSize = true;
        lblFooterTitle.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
        lblFooterTitle.ForeColor = Color.FromArgb(161, 161, 170);
        lblFooterTitle.Location = new Point(12, 9);
        lblFooterTitle.Name = "lblFooterTitle";
        lblFooterTitle.Size = new Size(185, 15);
        lblFooterTitle.TabIndex = 1;
        lblFooterTitle.Text = "Execution && Network Activity Log";
        // 
        // txtActivityLog
        // 
        txtActivityLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtActivityLog.BackColor = Color.FromArgb(18, 18, 20);
        txtActivityLog.BorderStyle = BorderStyle.FixedSingle;
        txtActivityLog.Font = new Font("Consolas", 8.5F);
        txtActivityLog.ForeColor = Color.FromArgb(228, 228, 231);
        txtActivityLog.Location = new Point(12, 33);
        txtActivityLog.Multiline = true;
        txtActivityLog.Name = "txtActivityLog";
        txtActivityLog.ReadOnly = true;
        txtActivityLog.ScrollBars = ScrollBars.Vertical;
        txtActivityLog.Size = new Size(760, 132);
        txtActivityLog.TabIndex = 0;
        // 
        // panelMainContent
        // 
        panelMainContent.BackColor = Color.FromArgb(248, 249, 251);
        panelMainContent.Controls.Add(tabControlViews);
        panelMainContent.Dock = DockStyle.Fill;
        panelMainContent.Location = new Point(220, 0);
        panelMainContent.Name = "panelMainContent";
        panelMainContent.Size = new Size(784, 545);
        panelMainContent.TabIndex = 2;
        // 
        // tabControlViews
        // 
        tabControlViews.Appearance = TabAppearance.FlatButtons;
        tabControlViews.Controls.Add(tabDashboard);
        tabControlViews.Controls.Add(tabAdapters);
        tabControlViews.Controls.Add(tabProxy);
        tabControlViews.Controls.Add(tabDownloader);
        tabControlViews.Controls.Add(tabSettings);
        tabControlViews.Dock = DockStyle.Fill;
        tabControlViews.ItemSize = new Size(0, 1);
        tabControlViews.Location = new Point(0, 0);
        tabControlViews.Name = "tabControlViews";
        tabControlViews.SelectedIndex = 0;
        tabControlViews.Size = new Size(784, 545);
        tabControlViews.SizeMode = TabSizeMode.Fixed;
        tabControlViews.TabIndex = 0;
        // 
        // tabDashboard
        // 
        tabDashboard.BackColor = Color.FromArgb(248, 249, 251);
        tabDashboard.Controls.Add(grpProxyQuickCard);
        tabDashboard.Controls.Add(panelCardAdapters);
        tabDashboard.Controls.Add(panelCardUpload);
        tabDashboard.Controls.Add(panelCardDownload);
        tabDashboard.Location = new Point(4, 5);
        tabDashboard.Name = "tabDashboard";
        tabDashboard.Padding = new Padding(18);
        tabDashboard.Size = new Size(776, 536);
        tabDashboard.TabIndex = 0;
        tabDashboard.Text = "Dashboard";
        // 
        // grpProxyQuickCard
        // 
        grpProxyQuickCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpProxyQuickCard.BackColor = Color.White;
        grpProxyQuickCard.Controls.Add(lblDashProxySessions);
        grpProxyQuickCard.Controls.Add(btnDashRefreshAdapters);
        grpProxyQuickCard.Controls.Add(btnDashToggleProxy);
        grpProxyQuickCard.Controls.Add(lblDashProxyEngineState);
        grpProxyQuickCard.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        grpProxyQuickCard.ForeColor = Color.FromArgb(30, 41, 59);
        grpProxyQuickCard.Location = new Point(18, 140);
        grpProxyQuickCard.Name = "grpProxyQuickCard";
        grpProxyQuickCard.Padding = new Padding(15);
        grpProxyQuickCard.Size = new Size(740, 378);
        grpProxyQuickCard.TabIndex = 3;
        grpProxyQuickCard.TabStop = false;
        grpProxyQuickCard.Text = "SOCKS5 Proxy Engine Status";
        // 
        // lblDashProxySessions
        // 
        lblDashProxySessions.AutoSize = true;
        lblDashProxySessions.Font = new Font("Segoe UI", 9.5F);
        lblDashProxySessions.ForeColor = Color.FromArgb(100, 116, 139);
        lblDashProxySessions.Location = new Point(20, 75);
        lblDashProxySessions.Name = "lblDashProxySessions";
        lblDashProxySessions.Size = new Size(152, 17);
        lblDashProxySessions.TabIndex = 3;
        lblDashProxySessions.Text = "Active Socket Sessions: 0";
        // 
        // btnDashRefreshAdapters
        // 
        btnDashRefreshAdapters.BackColor = Color.White;
        btnDashRefreshAdapters.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        btnDashRefreshAdapters.FlatStyle = FlatStyle.Flat;
        btnDashRefreshAdapters.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        btnDashRefreshAdapters.ForeColor = Color.FromArgb(51, 65, 85);
        btnDashRefreshAdapters.Location = new Point(210, 115);
        btnDashRefreshAdapters.Name = "btnDashRefreshAdapters";
        btnDashRefreshAdapters.Size = new Size(160, 38);
        btnDashRefreshAdapters.TabIndex = 2;
        btnDashRefreshAdapters.Text = "↻  Refresh Adapters";
        btnDashRefreshAdapters.UseVisualStyleBackColor = false;
        btnDashRefreshAdapters.Click += btnRefreshAdapters_Click;
        // 
        // btnDashToggleProxy
        // 
        btnDashToggleProxy.BackColor = Color.FromArgb(37, 99, 235);
        btnDashToggleProxy.FlatAppearance.BorderSize = 0;
        btnDashToggleProxy.FlatStyle = FlatStyle.Flat;
        btnDashToggleProxy.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        btnDashToggleProxy.ForeColor = Color.White;
        btnDashToggleProxy.Location = new Point(20, 115);
        btnDashToggleProxy.Name = "btnDashToggleProxy";
        btnDashToggleProxy.Size = new Size(175, 38);
        btnDashToggleProxy.TabIndex = 1;
        btnDashToggleProxy.Text = "Toggle SOCKS5 Proxy";
        btnDashToggleProxy.UseVisualStyleBackColor = false;
        btnDashToggleProxy.Click += btnToggleProxy_Click;
        // 
        // lblDashProxyEngineState
        // 
        lblDashProxyEngineState.AutoSize = true;
        lblDashProxyEngineState.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDashProxyEngineState.ForeColor = Color.FromArgb(239, 68, 68);
        lblDashProxyEngineState.Location = new Point(18, 42);
        lblDashProxyEngineState.Name = "lblDashProxyEngineState";
        lblDashProxyEngineState.Size = new Size(122, 20);
        lblDashProxyEngineState.TabIndex = 0;
        lblDashProxyEngineState.Text = "Engine: Stopped";
        // 
        // panelCardAdapters
        // 
        panelCardAdapters.BackColor = Color.White;
        panelCardAdapters.BorderStyle = BorderStyle.FixedSingle;
        panelCardAdapters.Controls.Add(lblCardAdaptersSub);
        panelCardAdapters.Controls.Add(lblCardAdaptersVal);
        panelCardAdapters.Controls.Add(lblCardAdaptersTitle);
        panelCardAdapters.Location = new Point(518, 18);
        panelCardAdapters.Name = "panelCardAdapters";
        panelCardAdapters.Padding = new Padding(12);
        panelCardAdapters.Size = new Size(240, 105);
        panelCardAdapters.TabIndex = 2;
        // 
        // lblCardAdaptersSub
        // 
        lblCardAdaptersSub.AutoSize = true;
        lblCardAdaptersSub.Font = new Font("Segoe UI", 8.25F);
        lblCardAdaptersSub.ForeColor = Color.FromArgb(148, 163, 184);
        lblCardAdaptersSub.Location = new Point(12, 75);
        lblCardAdaptersSub.Name = "lblCardAdaptersSub";
        lblCardAdaptersSub.Size = new Size(128, 13);
        lblCardAdaptersSub.TabIndex = 2;
        lblCardAdaptersSub.Text = "Operational IPv4 routes";
        // 
        // lblCardAdaptersVal
        // 
        lblCardAdaptersVal.AutoSize = true;
        lblCardAdaptersVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCardAdaptersVal.ForeColor = Color.FromArgb(15, 23, 42);
        lblCardAdaptersVal.Location = new Point(10, 36);
        lblCardAdaptersVal.Name = "lblCardAdaptersVal";
        lblCardAdaptersVal.Size = new Size(28, 32);
        lblCardAdaptersVal.TabIndex = 1;
        lblCardAdaptersVal.Text = "0";
        // 
        // lblCardAdaptersTitle
        // 
        lblCardAdaptersTitle.AutoSize = true;
        lblCardAdaptersTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblCardAdaptersTitle.ForeColor = Color.FromArgb(100, 116, 139);
        lblCardAdaptersTitle.Location = new Point(12, 14);
        lblCardAdaptersTitle.Name = "lblCardAdaptersTitle";
        lblCardAdaptersTitle.Size = new Size(109, 15);
        lblCardAdaptersTitle.TabIndex = 0;
        lblCardAdaptersTitle.Text = "ACTIVE ADAPTERS";
        // 
        // panelCardUpload
        // 
        panelCardUpload.BackColor = Color.White;
        panelCardUpload.BorderStyle = BorderStyle.FixedSingle;
        panelCardUpload.Controls.Add(lblCardUploadSub);
        panelCardUpload.Controls.Add(lblCardUploadVal);
        panelCardUpload.Controls.Add(lblCardUploadTitle);
        panelCardUpload.Location = new Point(268, 18);
        panelCardUpload.Name = "panelCardUpload";
        panelCardUpload.Padding = new Padding(12);
        panelCardUpload.Size = new Size(240, 105);
        panelCardUpload.TabIndex = 1;
        // 
        // lblCardUploadSub
        // 
        lblCardUploadSub.AutoSize = true;
        lblCardUploadSub.Font = new Font("Segoe UI", 8.25F);
        lblCardUploadSub.ForeColor = Color.FromArgb(148, 163, 184);
        lblCardUploadSub.Location = new Point(12, 75);
        lblCardUploadSub.Name = "lblCardUploadSub";
        lblCardUploadSub.Size = new Size(132, 13);
        lblCardUploadSub.TabIndex = 2;
        lblCardUploadSub.Text = "Aggregated throughput";
        // 
        // lblCardUploadVal
        // 
        lblCardUploadVal.AutoSize = true;
        lblCardUploadVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCardUploadVal.ForeColor = Color.FromArgb(16, 185, 129);
        lblCardUploadVal.Location = new Point(10, 36);
        lblCardUploadVal.Name = "lblCardUploadVal";
        lblCardUploadVal.Size = new Size(93, 32);
        lblCardUploadVal.TabIndex = 1;
        lblCardUploadVal.Text = "0.0 B/s";
        // 
        // lblCardUploadTitle
        // 
        lblCardUploadTitle.AutoSize = true;
        lblCardUploadTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblCardUploadTitle.ForeColor = Color.FromArgb(100, 116, 139);
        lblCardUploadTitle.Location = new Point(12, 14);
        lblCardUploadTitle.Name = "lblCardUploadTitle";
        lblCardUploadTitle.Size = new Size(130, 15);
        lblCardUploadTitle.TabIndex = 0;
        lblCardUploadTitle.Text = "TOTAL UPLOAD SPEED";
        // 
        // panelCardDownload
        // 
        panelCardDownload.BackColor = Color.White;
        panelCardDownload.BorderStyle = BorderStyle.FixedSingle;
        panelCardDownload.Controls.Add(lblCardDownloadSub);
        panelCardDownload.Controls.Add(lblCardDownloadVal);
        panelCardDownload.Controls.Add(lblCardDownloadTitle);
        panelCardDownload.Location = new Point(18, 18);
        panelCardDownload.Name = "panelCardDownload";
        panelCardDownload.Padding = new Padding(12);
        panelCardDownload.Size = new Size(240, 105);
        panelCardDownload.TabIndex = 0;
        // 
        // lblCardDownloadSub
        // 
        lblCardDownloadSub.AutoSize = true;
        lblCardDownloadSub.Font = new Font("Segoe UI", 8.25F);
        lblCardDownloadSub.ForeColor = Color.FromArgb(148, 163, 184);
        lblCardDownloadSub.Location = new Point(12, 75);
        lblCardDownloadSub.Name = "lblCardDownloadSub";
        lblCardDownloadSub.Size = new Size(132, 13);
        lblCardDownloadSub.TabIndex = 2;
        lblCardDownloadSub.Text = "Aggregated throughput";
        // 
        // lblCardDownloadVal
        // 
        lblCardDownloadVal.AutoSize = true;
        lblCardDownloadVal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCardDownloadVal.ForeColor = Color.FromArgb(37, 99, 235);
        lblCardDownloadVal.Location = new Point(10, 36);
        lblCardDownloadVal.Name = "lblCardDownloadVal";
        lblCardDownloadVal.Size = new Size(93, 32);
        lblCardDownloadVal.TabIndex = 1;
        lblCardDownloadVal.Text = "0.0 B/s";
        // 
        // lblCardDownloadTitle
        // 
        lblCardDownloadTitle.AutoSize = true;
        lblCardDownloadTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblCardDownloadTitle.ForeColor = Color.FromArgb(100, 116, 139);
        lblCardDownloadTitle.Location = new Point(12, 14);
        lblCardDownloadTitle.Name = "lblCardDownloadTitle";
        lblCardDownloadTitle.Size = new Size(154, 15);
        lblCardDownloadTitle.TabIndex = 0;
        lblCardDownloadTitle.Text = "TOTAL DOWNLOAD SPEED";
        // 
        // tabAdapters
        // 
        tabAdapters.BackColor = Color.FromArgb(248, 249, 251);
        tabAdapters.Controls.Add(dgvAdapters);
        tabAdapters.Controls.Add(panelAdaptersTop);
        tabAdapters.Location = new Point(4, 5);
        tabAdapters.Name = "tabAdapters";
        tabAdapters.Padding = new Padding(18);
        tabAdapters.Size = new Size(776, 536);
        tabAdapters.TabIndex = 1;
        tabAdapters.Text = "Adapters";
        // 
        // dgvAdapters
        // 
        dgvAdapters.AllowUserToAddRows = false;
        dgvAdapters.AllowUserToDeleteRows = false;
        dgvAdapters.AllowUserToResizeRows = false;
        dgvAdapters.BackgroundColor = Color.White;
        dgvAdapters.BorderStyle = BorderStyle.None;
        dgvAdapters.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvAdapters.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(241, 245, 249);
        dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(51, 65, 85);
        dataGridViewCellStyle1.Padding = new Padding(4);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(241, 245, 249);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(51, 65, 85);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvAdapters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvAdapters.ColumnHeadersHeight = 36;
        dgvAdapters.Columns.AddRange(new DataGridViewColumn[] { colActive, colName, colIp, colType, colStatus, colDownSpeed, colUpSpeed, colSessions });
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.White;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(30, 41, 59);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(238, 242, 255);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 41, 59);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvAdapters.DefaultCellStyle = dataGridViewCellStyle2;
        dgvAdapters.Dock = DockStyle.Fill;
        dgvAdapters.EnableHeadersVisualStyles = false;
        dgvAdapters.GridColor = Color.FromArgb(226, 232, 240);
        dgvAdapters.Location = new Point(18, 63);
        dgvAdapters.MultiSelect = false;
        dgvAdapters.Name = "dgvAdapters";
        dgvAdapters.RowHeadersVisible = false;
        dgvAdapters.RowTemplate.Height = 32;
        dgvAdapters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAdapters.Size = new Size(740, 455);
        dgvAdapters.TabIndex = 1;
        dgvAdapters.CellValueChanged += dgvAdapters_CellValueChanged;
        dgvAdapters.CurrentCellDirtyStateChanged += dgvAdapters_CurrentCellDirtyStateChanged;
        // 
        // colActive
        // 
        colActive.HeaderText = "Active";
        colActive.Name = "colActive";
        colActive.Width = 60;
        // 
        // colName
        // 
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colName.HeaderText = "Adapter Name";
        colName.Name = "colName";
        colName.ReadOnly = true;
        // 
        // colIp
        // 
        colIp.HeaderText = "Local IPv4";
        colIp.Name = "colIp";
        colIp.ReadOnly = true;
        colIp.Width = 120;
        // 
        // colType
        // 
        colType.HeaderText = "Type";
        colType.Name = "colType";
        colType.ReadOnly = true;
        // 
        // colStatus
        // 
        colStatus.HeaderText = "Status";
        colStatus.Name = "colStatus";
        colStatus.ReadOnly = true;
        colStatus.Width = 70;
        // 
        // colDownSpeed
        // 
        colDownSpeed.HeaderText = "Download Speed";
        colDownSpeed.Name = "colDownSpeed";
        colDownSpeed.ReadOnly = true;
        colDownSpeed.Width = 110;
        // 
        // colUpSpeed
        // 
        colUpSpeed.HeaderText = "Upload Speed";
        colUpSpeed.Name = "colUpSpeed";
        colUpSpeed.ReadOnly = true;
        colUpSpeed.Width = 110;
        // 
        // colSessions
        // 
        colSessions.HeaderText = "Sessions";
        colSessions.Name = "colSessions";
        colSessions.ReadOnly = true;
        colSessions.Width = 70;
        // 
        // panelAdaptersTop
        // 
        panelAdaptersTop.Controls.Add(btnSelectAllAdapters);
        panelAdaptersTop.Controls.Add(btnRefreshAdaptersGrid);
        panelAdaptersTop.Controls.Add(lblAdaptersViewTitle);
        panelAdaptersTop.Dock = DockStyle.Top;
        panelAdaptersTop.Location = new Point(18, 18);
        panelAdaptersTop.Name = "panelAdaptersTop";
        panelAdaptersTop.Size = new Size(740, 45);
        panelAdaptersTop.TabIndex = 0;
        // 
        // btnSelectAllAdapters
        // 
        btnSelectAllAdapters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSelectAllAdapters.BackColor = Color.White;
        btnSelectAllAdapters.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        btnSelectAllAdapters.FlatStyle = FlatStyle.Flat;
        btnSelectAllAdapters.Font = new Font("Segoe UI", 9F);
        btnSelectAllAdapters.ForeColor = Color.FromArgb(51, 65, 85);
        btnSelectAllAdapters.Location = new Point(475, 6);
        btnSelectAllAdapters.Name = "btnSelectAllAdapters";
        btnSelectAllAdapters.Size = new Size(105, 30);
        btnSelectAllAdapters.TabIndex = 2;
        btnSelectAllAdapters.Text = "Toggle All";
        btnSelectAllAdapters.UseVisualStyleBackColor = false;
        btnSelectAllAdapters.Click += btnSelectAllAdapters_Click;
        // 
        // btnRefreshAdaptersGrid
        // 
        btnRefreshAdaptersGrid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefreshAdaptersGrid.BackColor = Color.White;
        btnRefreshAdaptersGrid.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        btnRefreshAdaptersGrid.FlatStyle = FlatStyle.Flat;
        btnRefreshAdaptersGrid.Font = new Font("Segoe UI", 9F);
        btnRefreshAdaptersGrid.ForeColor = Color.FromArgb(51, 65, 85);
        btnRefreshAdaptersGrid.Location = new Point(590, 6);
        btnRefreshAdaptersGrid.Name = "btnRefreshAdaptersGrid";
        btnRefreshAdaptersGrid.Size = new Size(145, 30);
        btnRefreshAdaptersGrid.TabIndex = 1;
        btnRefreshAdaptersGrid.Text = "↻  Refresh Adapters";
        btnRefreshAdaptersGrid.UseVisualStyleBackColor = false;
        btnRefreshAdaptersGrid.Click += btnRefreshAdapters_Click;
        // 
        // lblAdaptersViewTitle
        // 
        lblAdaptersViewTitle.AutoSize = true;
        lblAdaptersViewTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblAdaptersViewTitle.ForeColor = Color.FromArgb(15, 23, 42);
        lblAdaptersViewTitle.Location = new Point(0, 10);
        lblAdaptersViewTitle.Name = "lblAdaptersViewTitle";
        lblAdaptersViewTitle.Size = new Size(211, 21);
        lblAdaptersViewTitle.TabIndex = 0;
        lblAdaptersViewTitle.Text = "Bonded Network Adapters";
        // 
        // tabProxy
        // 
        tabProxy.BackColor = Color.FromArgb(248, 249, 251);
        tabProxy.Controls.Add(grpProxyActiveAdapters);
        tabProxy.Controls.Add(grpProxyConfig);
        tabProxy.Location = new Point(4, 5);
        tabProxy.Name = "tabProxy";
        tabProxy.Padding = new Padding(18);
        tabProxy.Size = new Size(776, 536);
        tabProxy.TabIndex = 2;
        tabProxy.Text = "SOCKS5 Proxy";
        // 
        // grpProxyActiveAdapters
        // 
        grpProxyActiveAdapters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpProxyActiveAdapters.BackColor = Color.White;
        grpProxyActiveAdapters.Controls.Add(lblProxyRoutingNotice);
        grpProxyActiveAdapters.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpProxyActiveAdapters.ForeColor = Color.FromArgb(30, 41, 59);
        grpProxyActiveAdapters.Location = new Point(18, 255);
        grpProxyActiveAdapters.Name = "grpProxyActiveAdapters";
        grpProxyActiveAdapters.Padding = new Padding(15);
        grpProxyActiveAdapters.Size = new Size(740, 263);
        grpProxyActiveAdapters.TabIndex = 1;
        grpProxyActiveAdapters.TabStop = false;
        grpProxyActiveAdapters.Text = "Bonding Route Distribution";
        // 
        // lblProxyRoutingNotice
        // 
        lblProxyRoutingNotice.Dock = DockStyle.Fill;
        lblProxyRoutingNotice.Font = new Font("Segoe UI", 9F);
        lblProxyRoutingNotice.ForeColor = Color.FromArgb(100, 116, 139);
        lblProxyRoutingNotice.Location = new Point(15, 32);
        lblProxyRoutingNotice.Name = "lblProxyRoutingNotice";
        lblProxyRoutingNotice.Size = new Size(710, 216);
        lblProxyRoutingNotice.TabIndex = 0;
        lblProxyRoutingNotice.Text = resources.GetString("lblProxyRoutingNotice.Text");
        // 
        // grpProxyConfig
        // 
        grpProxyConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpProxyConfig.BackColor = Color.White;
        grpProxyConfig.Controls.Add(lblProxyActiveSessionsVal);
        grpProxyConfig.Controls.Add(lblProxyActiveSessions);
        grpProxyConfig.Controls.Add(btnProxyToggle);
        grpProxyConfig.Controls.Add(lblProxyStateStatus);
        grpProxyConfig.Controls.Add(cmbProxyStrategy);
        grpProxyConfig.Controls.Add(lblProxyStrategy);
        grpProxyConfig.Controls.Add(nudProxyPort);
        grpProxyConfig.Controls.Add(lblProxyPort);
        grpProxyConfig.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpProxyConfig.ForeColor = Color.FromArgb(30, 41, 59);
        grpProxyConfig.Location = new Point(18, 18);
        grpProxyConfig.Name = "grpProxyConfig";
        grpProxyConfig.Padding = new Padding(15);
        grpProxyConfig.Size = new Size(740, 220);
        grpProxyConfig.TabIndex = 0;
        grpProxyConfig.TabStop = false;
        grpProxyConfig.Text = "SOCKS5 Proxy Server Settings";
        // 
        // lblProxyActiveSessionsVal
        // 
        lblProxyActiveSessionsVal.AutoSize = true;
        lblProxyActiveSessionsVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblProxyActiveSessionsVal.ForeColor = Color.FromArgb(15, 23, 42);
        lblProxyActiveSessionsVal.Location = new Point(175, 125);
        lblProxyActiveSessionsVal.Name = "lblProxyActiveSessionsVal";
        lblProxyActiveSessionsVal.Size = new Size(15, 17);
        lblProxyActiveSessionsVal.TabIndex = 7;
        lblProxyActiveSessionsVal.Text = "0";
        // 
        // lblProxyActiveSessions
        // 
        lblProxyActiveSessions.AutoSize = true;
        lblProxyActiveSessions.Font = new Font("Segoe UI", 9F);
        lblProxyActiveSessions.ForeColor = Color.FromArgb(100, 116, 139);
        lblProxyActiveSessions.Location = new Point(20, 126);
        lblProxyActiveSessions.Name = "lblProxyActiveSessions";
        lblProxyActiveSessions.Size = new Size(128, 15);
        lblProxyActiveSessions.TabIndex = 6;
        lblProxyActiveSessions.Text = "Active Socket Sessions:";
        // 
        // btnProxyToggle
        // 
        btnProxyToggle.BackColor = Color.FromArgb(37, 99, 235);
        btnProxyToggle.FlatAppearance.BorderSize = 0;
        btnProxyToggle.FlatStyle = FlatStyle.Flat;
        btnProxyToggle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        btnProxyToggle.ForeColor = Color.White;
        btnProxyToggle.Location = new Point(20, 160);
        btnProxyToggle.Name = "btnProxyToggle";
        btnProxyToggle.Size = new Size(150, 38);
        btnProxyToggle.TabIndex = 5;
        btnProxyToggle.Text = "Start Proxy";
        btnProxyToggle.UseVisualStyleBackColor = false;
        btnProxyToggle.Click += btnToggleProxy_Click;
        // 
        // lblProxyStateStatus
        // 
        lblProxyStateStatus.AutoSize = true;
        lblProxyStateStatus.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblProxyStateStatus.ForeColor = Color.FromArgb(239, 68, 68);
        lblProxyStateStatus.Location = new Point(185, 171);
        lblProxyStateStatus.Name = "lblProxyStateStatus";
        lblProxyStateStatus.Size = new Size(108, 17);
        lblProxyStateStatus.TabIndex = 4;
        lblProxyStateStatus.Text = "Status: STOPPED";
        // 
        // cmbProxyStrategy
        // 
        cmbProxyStrategy.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbProxyStrategy.Font = new Font("Segoe UI", 9.5F);
        cmbProxyStrategy.FormattingEnabled = true;
        cmbProxyStrategy.Items.AddRange(new object[] { "Round-robin (Alternating)", "Least connections (Dynamic load)" });
        cmbProxyStrategy.Location = new Point(175, 78);
        cmbProxyStrategy.Name = "cmbProxyStrategy";
        cmbProxyStrategy.Size = new Size(260, 25);
        cmbProxyStrategy.TabIndex = 3;
        cmbProxyStrategy.SelectedIndexChanged += cmbProxyStrategy_SelectedIndexChanged;
        // 
        // lblProxyStrategy
        // 
        lblProxyStrategy.AutoSize = true;
        lblProxyStrategy.Font = new Font("Segoe UI", 9F);
        lblProxyStrategy.Location = new Point(20, 82);
        lblProxyStrategy.Name = "lblProxyStrategy";
        lblProxyStrategy.Size = new Size(97, 15);
        lblProxyStrategy.TabIndex = 2;
        lblProxyStrategy.Text = "Binding Strategy:";
        // 
        // nudProxyPort
        // 
        nudProxyPort.Font = new Font("Segoe UI", 9.5F);
        nudProxyPort.Location = new Point(175, 36);
        nudProxyPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        nudProxyPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudProxyPort.Name = "nudProxyPort";
        nudProxyPort.Size = new Size(95, 24);
        nudProxyPort.TabIndex = 1;
        nudProxyPort.Value = new decimal(new int[] { 10808, 0, 0, 0 });
        // 
        // lblProxyPort
        // 
        lblProxyPort.AutoSize = true;
        lblProxyPort.Font = new Font("Segoe UI", 9F);
        lblProxyPort.Location = new Point(20, 40);
        lblProxyPort.Name = "lblProxyPort";
        lblProxyPort.Size = new Size(108, 15);
        lblProxyPort.TabIndex = 0;
        lblProxyPort.Text = "Port Binding (TCP):";
        // 
        // tabDownloader
        // 
        tabDownloader.BackColor = Color.FromArgb(248, 249, 251);
        tabDownloader.Controls.Add(grpDownloaderChunks);
        tabDownloader.Controls.Add(grpDownloaderConfig);
        tabDownloader.Location = new Point(4, 5);
        tabDownloader.Name = "tabDownloader";
        tabDownloader.Padding = new Padding(18);
        tabDownloader.Size = new Size(776, 536);
        tabDownloader.TabIndex = 3;
        tabDownloader.Text = "Downloader";
        // 
        // grpDownloaderChunks
        // 
        grpDownloaderChunks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpDownloaderChunks.BackColor = Color.White;
        grpDownloaderChunks.Controls.Add(lvChunkStats);
        grpDownloaderChunks.Controls.Add(lblOverallDownloadSpeed);
        grpDownloaderChunks.Controls.Add(lblOverallDownloadProgress);
        grpDownloaderChunks.Controls.Add(pbDownloader);
        grpDownloaderChunks.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpDownloaderChunks.ForeColor = Color.FromArgb(30, 41, 59);
        grpDownloaderChunks.Location = new Point(18, 195);
        grpDownloaderChunks.Name = "grpDownloaderChunks";
        grpDownloaderChunks.Padding = new Padding(12);
        grpDownloaderChunks.Size = new Size(740, 323);
        grpDownloaderChunks.TabIndex = 1;
        grpDownloaderChunks.TabStop = false;
        grpDownloaderChunks.Text = "Download Progress && Chunk Distribution";
        // 
        // lvChunkStats
        // 
        lvChunkStats.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lvChunkStats.BorderStyle = BorderStyle.FixedSingle;
        lvChunkStats.Columns.AddRange(new ColumnHeader[] { colChunkId, colChunkAdapter, colChunkIp, colChunkSpeed, colChunkStatus });
        lvChunkStats.Font = new Font("Segoe UI", 9F);
        lvChunkStats.FullRowSelect = true;
        lvChunkStats.GridLines = true;
        lvChunkStats.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        lvChunkStats.Location = new Point(12, 85);
        lvChunkStats.Name = "lvChunkStats";
        lvChunkStats.Size = new Size(716, 226);
        lvChunkStats.TabIndex = 3;
        lvChunkStats.UseCompatibleStateImageBehavior = false;
        lvChunkStats.View = View.Details;
        // 
        // colChunkId
        // 
        colChunkId.Text = "Chunk";
        colChunkId.Width = 80;
        // 
        // colChunkAdapter
        // 
        colChunkAdapter.Text = "Bound Adapter";
        colChunkAdapter.Width = 180;
        // 
        // colChunkIp
        // 
        colChunkIp.Text = "Local IP";
        colChunkIp.Width = 130;
        // 
        // colChunkSpeed
        // 
        colChunkSpeed.Text = "Speed";
        colChunkSpeed.Width = 140;
        // 
        // colChunkStatus
        // 
        colChunkStatus.Text = "Status";
        colChunkStatus.Width = 150;
        // 
        // lblOverallDownloadSpeed
        // 
        lblOverallDownloadSpeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOverallDownloadSpeed.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblOverallDownloadSpeed.ForeColor = Color.FromArgb(37, 99, 235);
        lblOverallDownloadSpeed.Location = new Point(478, 60);
        lblOverallDownloadSpeed.Name = "lblOverallDownloadSpeed";
        lblOverallDownloadSpeed.Size = new Size(250, 18);
        lblOverallDownloadSpeed.TabIndex = 2;
        lblOverallDownloadSpeed.Text = "Speed: 0.00 MB/s";
        lblOverallDownloadSpeed.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblOverallDownloadProgress
        // 
        lblOverallDownloadProgress.AutoSize = true;
        lblOverallDownloadProgress.Font = new Font("Segoe UI", 9F);
        lblOverallDownloadProgress.ForeColor = Color.FromArgb(100, 116, 139);
        lblOverallDownloadProgress.Location = new Point(12, 62);
        lblOverallDownloadProgress.Name = "lblOverallDownloadProgress";
        lblOverallDownloadProgress.Size = new Size(39, 15);
        lblOverallDownloadProgress.TabIndex = 1;
        lblOverallDownloadProgress.Text = "Ready";
        // 
        // pbDownloader
        // 
        pbDownloader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pbDownloader.Location = new Point(12, 28);
        pbDownloader.Name = "pbDownloader";
        pbDownloader.Size = new Size(716, 24);
        pbDownloader.TabIndex = 0;
        // 
        // grpDownloaderConfig
        // 
        grpDownloaderConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpDownloaderConfig.BackColor = Color.White;
        grpDownloaderConfig.Controls.Add(btnCancelDownload);
        grpDownloaderConfig.Controls.Add(btnStartDownload);
        grpDownloaderConfig.Controls.Add(nudChunkCount);
        grpDownloaderConfig.Controls.Add(lblChunkCount);
        grpDownloaderConfig.Controls.Add(btnBrowseDest);
        grpDownloaderConfig.Controls.Add(txtDestPath);
        grpDownloaderConfig.Controls.Add(lblDestPath);
        grpDownloaderConfig.Controls.Add(txtDownloadUrl);
        grpDownloaderConfig.Controls.Add(lblDownloadUrl);
        grpDownloaderConfig.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpDownloaderConfig.ForeColor = Color.FromArgb(30, 41, 59);
        grpDownloaderConfig.Location = new Point(18, 18);
        grpDownloaderConfig.Name = "grpDownloaderConfig";
        grpDownloaderConfig.Padding = new Padding(12);
        grpDownloaderConfig.Size = new Size(740, 165);
        grpDownloaderConfig.TabIndex = 0;
        grpDownloaderConfig.TabStop = false;
        grpDownloaderConfig.Text = "Multi-Adapter Range Download Setup";
        // 
        // btnCancelDownload
        // 
        btnCancelDownload.BackColor = Color.White;
        btnCancelDownload.Enabled = false;
        btnCancelDownload.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
        btnCancelDownload.FlatStyle = FlatStyle.Flat;
        btnCancelDownload.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnCancelDownload.ForeColor = Color.FromArgb(239, 68, 68);
        btnCancelDownload.Location = new Point(400, 115);
        btnCancelDownload.Name = "btnCancelDownload";
        btnCancelDownload.Size = new Size(95, 32);
        btnCancelDownload.TabIndex = 8;
        btnCancelDownload.Text = "Cancel";
        btnCancelDownload.UseVisualStyleBackColor = false;
        btnCancelDownload.Click += btnCancelDownload_Click;
        // 
        // btnStartDownload
        // 
        btnStartDownload.BackColor = Color.FromArgb(37, 99, 235);
        btnStartDownload.FlatAppearance.BorderSize = 0;
        btnStartDownload.FlatStyle = FlatStyle.Flat;
        btnStartDownload.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnStartDownload.ForeColor = Color.White;
        btnStartDownload.Location = new Point(245, 115);
        btnStartDownload.Name = "btnStartDownload";
        btnStartDownload.Size = new Size(145, 32);
        btnStartDownload.TabIndex = 7;
        btnStartDownload.Text = "Start Download";
        btnStartDownload.UseVisualStyleBackColor = false;
        btnStartDownload.Click += btnStartDownload_Click;
        // 
        // nudChunkCount
        // 
        nudChunkCount.Font = new Font("Segoe UI", 9.5F);
        nudChunkCount.Location = new Point(125, 120);
        nudChunkCount.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
        nudChunkCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudChunkCount.Name = "nudChunkCount";
        nudChunkCount.Size = new Size(65, 24);
        nudChunkCount.TabIndex = 6;
        nudChunkCount.Value = new decimal(new int[] { 4, 0, 0, 0 });
        // 
        // lblChunkCount
        // 
        lblChunkCount.AutoSize = true;
        lblChunkCount.Font = new Font("Segoe UI", 9F);
        lblChunkCount.Location = new Point(15, 123);
        lblChunkCount.Name = "lblChunkCount";
        lblChunkCount.Size = new Size(91, 15);
        lblChunkCount.TabIndex = 5;
        lblChunkCount.Text = "Parallel Chunks:";
        // 
        // btnBrowseDest
        // 
        btnBrowseDest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBrowseDest.BackColor = Color.White;
        btnBrowseDest.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        btnBrowseDest.FlatStyle = FlatStyle.Flat;
        btnBrowseDest.Font = new Font("Segoe UI", 9F);
        btnBrowseDest.ForeColor = Color.FromArgb(51, 65, 85);
        btnBrowseDest.Location = new Point(645, 73);
        btnBrowseDest.Name = "btnBrowseDest";
        btnBrowseDest.Size = new Size(83, 26);
        btnBrowseDest.TabIndex = 4;
        btnBrowseDest.Text = "Browse...";
        btnBrowseDest.UseVisualStyleBackColor = false;
        btnBrowseDest.Click += btnBrowseDest_Click;
        // 
        // txtDestPath
        // 
        txtDestPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDestPath.Font = new Font("Segoe UI", 9.5F);
        txtDestPath.Location = new Point(125, 74);
        txtDestPath.Name = "txtDestPath";
        txtDestPath.Size = new Size(514, 24);
        txtDestPath.TabIndex = 3;
        // 
        // lblDestPath
        // 
        lblDestPath.AutoSize = true;
        lblDestPath.Font = new Font("Segoe UI", 9F);
        lblDestPath.Location = new Point(15, 77);
        lblDestPath.Name = "lblDestPath";
        lblDestPath.Size = new Size(61, 15);
        lblDestPath.TabIndex = 2;
        lblDestPath.Text = "Save Path:";
        // 
        // txtDownloadUrl
        // 
        txtDownloadUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDownloadUrl.Font = new Font("Segoe UI", 9.5F);
        txtDownloadUrl.Location = new Point(125, 33);
        txtDownloadUrl.Name = "txtDownloadUrl";
        txtDownloadUrl.Size = new Size(603, 24);
        txtDownloadUrl.TabIndex = 1;
        // 
        // lblDownloadUrl
        // 
        lblDownloadUrl.AutoSize = true;
        lblDownloadUrl.Font = new Font("Segoe UI", 9F);
        lblDownloadUrl.Location = new Point(15, 36);
        lblDownloadUrl.Name = "lblDownloadUrl";
        lblDownloadUrl.Size = new Size(67, 15);
        lblDownloadUrl.TabIndex = 0;
        lblDownloadUrl.Text = "Target URL:";
        // 
        // tabSettings
        // 
        tabSettings.BackColor = Color.FromArgb(248, 249, 251);
        tabSettings.Controls.Add(grpUpdates);
        tabSettings.Controls.Add(grpGeneralSettings);
        tabSettings.Location = new Point(4, 5);
        tabSettings.Name = "tabSettings";
        tabSettings.Padding = new Padding(18);
        tabSettings.Size = new Size(776, 536);
        tabSettings.TabIndex = 4;
        tabSettings.Text = "Settings";
        // 
        // grpUpdates
        // 
        grpUpdates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpUpdates.BackColor = Color.White;
        grpUpdates.Controls.Add(lblUpdateStatus);
        grpUpdates.Controls.Add(btnCheckUpdates);
        grpUpdates.Controls.Add(txtRepoUrl);
        grpUpdates.Controls.Add(lblRepoUrl);
        grpUpdates.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpUpdates.ForeColor = Color.FromArgb(30, 41, 59);
        grpUpdates.Location = new Point(18, 260);
        grpUpdates.Name = "grpUpdates";
        grpUpdates.Padding = new Padding(15);
        grpUpdates.Size = new Size(740, 150);
        grpUpdates.TabIndex = 1;
        grpUpdates.TabStop = false;
        grpUpdates.Text = "Velopack Software Updates";
        // 
        // lblUpdateStatus
        // 
        lblUpdateStatus.AutoSize = true;
        lblUpdateStatus.Font = new Font("Segoe UI", 9F);
        lblUpdateStatus.ForeColor = Color.FromArgb(100, 116, 139);
        lblUpdateStatus.Location = new Point(215, 96);
        lblUpdateStatus.Name = "lblUpdateStatus";
        lblUpdateStatus.Size = new Size(150, 15);
        lblUpdateStatus.TabIndex = 3;
        lblUpdateStatus.Text = "Ready to check for updates";
        // 
        // btnCheckUpdates
        // 
        btnCheckUpdates.BackColor = Color.FromArgb(37, 99, 235);
        btnCheckUpdates.FlatAppearance.BorderSize = 0;
        btnCheckUpdates.FlatStyle = FlatStyle.Flat;
        btnCheckUpdates.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnCheckUpdates.ForeColor = Color.White;
        btnCheckUpdates.Location = new Point(20, 88);
        btnCheckUpdates.Name = "btnCheckUpdates";
        btnCheckUpdates.Size = new Size(175, 32);
        btnCheckUpdates.TabIndex = 2;
        btnCheckUpdates.Text = "Check for Updates";
        btnCheckUpdates.UseVisualStyleBackColor = false;
        btnCheckUpdates.Click += btnCheckUpdates_Click;
        // 
        // txtRepoUrl
        // 
        txtRepoUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtRepoUrl.Font = new Font("Segoe UI", 9.5F);
        txtRepoUrl.Location = new Point(170, 36);
        txtRepoUrl.Name = "txtRepoUrl";
        txtRepoUrl.Size = new Size(550, 24);
        txtRepoUrl.TabIndex = 1;
        txtRepoUrl.Text = "https://github.com/your-username/NetBondDispatcher";
        // 
        // lblRepoUrl
        // 
        lblRepoUrl.AutoSize = true;
        lblRepoUrl.Font = new Font("Segoe UI", 9F);
        lblRepoUrl.Location = new Point(18, 40);
        lblRepoUrl.Name = "lblRepoUrl";
        lblRepoUrl.Size = new Size(131, 15);
        lblRepoUrl.TabIndex = 0;
        lblRepoUrl.Text = "GitHub Repository URL:";
        // 
        // grpGeneralSettings
        // 
        grpGeneralSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpGeneralSettings.BackColor = Color.White;
        grpGeneralSettings.Controls.Add(btnSaveSettings);
        grpGeneralSettings.Controls.Add(chkAutoStartProxy);
        grpGeneralSettings.Controls.Add(chkMinimizeOnClose);
        grpGeneralSettings.Controls.Add(chkStartMinimized);
        grpGeneralSettings.Controls.Add(chkStartWithWindows);
        grpGeneralSettings.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        grpGeneralSettings.ForeColor = Color.FromArgb(30, 41, 59);
        grpGeneralSettings.Location = new Point(18, 18);
        grpGeneralSettings.Name = "grpGeneralSettings";
        grpGeneralSettings.Padding = new Padding(15);
        grpGeneralSettings.Size = new Size(740, 225);
        grpGeneralSettings.TabIndex = 0;
        grpGeneralSettings.TabStop = false;
        grpGeneralSettings.Text = "Startup && Behavior Preferences";
        // 
        // btnSaveSettings
        // 
        btnSaveSettings.BackColor = Color.FromArgb(16, 185, 129);
        btnSaveSettings.FlatAppearance.BorderSize = 0;
        btnSaveSettings.FlatStyle = FlatStyle.Flat;
        btnSaveSettings.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnSaveSettings.ForeColor = Color.White;
        btnSaveSettings.Location = new Point(20, 175);
        btnSaveSettings.Name = "btnSaveSettings";
        btnSaveSettings.Size = new Size(140, 32);
        btnSaveSettings.TabIndex = 4;
        btnSaveSettings.Text = "Save Settings";
        btnSaveSettings.UseVisualStyleBackColor = false;
        btnSaveSettings.Click += btnSaveSettings_Click;
        // 
        // chkAutoStartProxy
        // 
        chkAutoStartProxy.AutoSize = true;
        chkAutoStartProxy.Font = new Font("Segoe UI", 9F);
        chkAutoStartProxy.Location = new Point(20, 135);
        chkAutoStartProxy.Name = "chkAutoStartProxy";
        chkAutoStartProxy.Size = new Size(249, 19);
        chkAutoStartProxy.TabIndex = 3;
        chkAutoStartProxy.Text = "Auto-start SOCKS5 Proxy Server on startup";
        chkAutoStartProxy.UseVisualStyleBackColor = true;
        // 
        // chkMinimizeOnClose
        // 
        chkMinimizeOnClose.AutoSize = true;
        chkMinimizeOnClose.Font = new Font("Segoe UI", 9F);
        chkMinimizeOnClose.Location = new Point(20, 100);
        chkMinimizeOnClose.Name = "chkMinimizeOnClose";
        chkMinimizeOnClose.Size = new Size(270, 19);
        chkMinimizeOnClose.TabIndex = 2;
        chkMinimizeOnClose.Text = "Minimize to system tray when closing window";
        chkMinimizeOnClose.UseVisualStyleBackColor = true;
        // 
        // chkStartMinimized
        // 
        chkStartMinimized.AutoSize = true;
        chkStartMinimized.Font = new Font("Segoe UI", 9F);
        chkStartMinimized.Location = new Point(20, 65);
        chkStartMinimized.Name = "chkStartMinimized";
        chkStartMinimized.Size = new Size(248, 19);
        chkStartMinimized.TabIndex = 1;
        chkStartMinimized.Text = "Start application minimized to system tray";
        chkStartMinimized.UseVisualStyleBackColor = true;
        // 
        // chkStartWithWindows
        // 
        chkStartWithWindows.AutoSize = true;
        chkStartWithWindows.Font = new Font("Segoe UI", 9F);
        chkStartWithWindows.Location = new Point(20, 30);
        chkStartWithWindows.Name = "chkStartWithWindows";
        chkStartWithWindows.Size = new Size(309, 19);
        chkStartWithWindows.TabIndex = 0;
        chkStartWithWindows.Text = "Start NetBondDispatcher automatically with Windows";
        chkStartWithWindows.UseVisualStyleBackColor = true;
        // 
        // notifyIcon
        // 
        notifyIcon.ContextMenuStrip = trayMenu;
        notifyIcon.Text = "NetBondDispatcher";
        notifyIcon.Visible = true;
        notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        // 
        // trayMenu
        // 
        trayMenu.Font = new Font("Segoe UI", 9F);
        trayMenu.Items.AddRange(new ToolStripItem[] { menuItemShow, menuSeparator1, menuItemToggleProxy, menuSeparator2, menuItemExit });
        trayMenu.Name = "trayMenu";
        trayMenu.Size = new Size(211, 82);
        // 
        // menuItemShow
        // 
        menuItemShow.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        menuItemShow.Name = "menuItemShow";
        menuItemShow.Size = new Size(210, 22);
        menuItemShow.Text = "Open NetBondDispatcher";
        menuItemShow.Click += menuItemShow_Click;
        // 
        // menuSeparator1
        // 
        menuSeparator1.Name = "menuSeparator1";
        menuSeparator1.Size = new Size(207, 6);
        // 
        // menuItemToggleProxy
        // 
        menuItemToggleProxy.Name = "menuItemToggleProxy";
        menuItemToggleProxy.Size = new Size(210, 22);
        menuItemToggleProxy.Text = "Start Proxy";
        menuItemToggleProxy.Click += menuItemToggleProxy_Click;
        // 
        // menuSeparator2
        // 
        menuSeparator2.Name = "menuSeparator2";
        menuSeparator2.Size = new Size(207, 6);
        // 
        // menuItemExit
        // 
        menuItemExit.Name = "menuItemExit";
        menuItemExit.Size = new Size(210, 22);
        menuItemExit.Text = "Exit";
        menuItemExit.Click += menuItemExit_Click;
        // 
        // timerSpeedCalculation
        // 
        timerSpeedCalculation.Interval = 1000;
        timerSpeedCalculation.Tick += timerSpeedCalculation_Tick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(248, 249, 251);
        ClientSize = new Size(1004, 720);
        Controls.Add(panelMainContent);
        Controls.Add(panelBottomFooter);
        Controls.Add(panelSidebar);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(950, 680);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "NetBond Dispatcher v1.0 - Multi-Adapter Network Aggregator";
        FormClosing += MainForm_FormClosing;
        Load += MainForm_Load;
        panelSidebar.ResumeLayout(false);
        panelBrand.ResumeLayout(false);
        panelBrand.PerformLayout();
        panelBottomFooter.ResumeLayout(false);
        panelBottomFooter.PerformLayout();
        panelMainContent.ResumeLayout(false);
        tabControlViews.ResumeLayout(false);
        tabDashboard.ResumeLayout(false);
        grpProxyQuickCard.ResumeLayout(false);
        grpProxyQuickCard.PerformLayout();
        panelCardAdapters.ResumeLayout(false);
        panelCardAdapters.PerformLayout();
        panelCardUpload.ResumeLayout(false);
        panelCardUpload.PerformLayout();
        panelCardDownload.ResumeLayout(false);
        panelCardDownload.PerformLayout();
        tabAdapters.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvAdapters).EndInit();
        panelAdaptersTop.ResumeLayout(false);
        panelAdaptersTop.PerformLayout();
        tabProxy.ResumeLayout(false);
        grpProxyActiveAdapters.ResumeLayout(false);
        grpProxyConfig.ResumeLayout(false);
        grpProxyConfig.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudProxyPort).EndInit();
        tabDownloader.ResumeLayout(false);
        grpDownloaderChunks.ResumeLayout(false);
        grpDownloaderChunks.PerformLayout();
        grpDownloaderConfig.ResumeLayout(false);
        grpDownloaderConfig.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudChunkCount).EndInit();
        tabSettings.ResumeLayout(false);
        grpUpdates.ResumeLayout(false);
        grpUpdates.PerformLayout();
        grpGeneralSettings.ResumeLayout(false);
        grpGeneralSettings.PerformLayout();
        trayMenu.ResumeLayout(false);
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelSidebar;
    private System.Windows.Forms.Panel panelBrand;
    private System.Windows.Forms.Label lblBrandTitle;
    private System.Windows.Forms.Label lblBrandSubtitle;
    private System.Windows.Forms.Button btnNavDashboard;
    private System.Windows.Forms.Button btnNavAdapters;
    private System.Windows.Forms.Button btnNavProxy;
    private System.Windows.Forms.Button btnNavDownloader;
    private System.Windows.Forms.Button btnNavSettings;
    private System.Windows.Forms.Panel panelBottomFooter;
    private System.Windows.Forms.TextBox txtActivityLog;
    private System.Windows.Forms.Label lblFooterTitle;
    private System.Windows.Forms.Button btnClearLogs;
    private System.Windows.Forms.Panel panelMainContent;
    private System.Windows.Forms.TabControl tabControlViews;
    private System.Windows.Forms.TabPage tabDashboard;
    private System.Windows.Forms.TabPage tabAdapters;
    private System.Windows.Forms.TabPage tabProxy;
    private System.Windows.Forms.TabPage tabDownloader;
    private System.Windows.Forms.TabPage tabSettings;
    private System.Windows.Forms.Panel panelCardDownload;
    private System.Windows.Forms.Label lblCardDownloadTitle;
    private System.Windows.Forms.Label lblCardDownloadVal;
    private System.Windows.Forms.Label lblCardDownloadSub;
    private System.Windows.Forms.Panel panelCardUpload;
    private System.Windows.Forms.Label lblCardUploadTitle;
    private System.Windows.Forms.Label lblCardUploadVal;
    private System.Windows.Forms.Label lblCardUploadSub;
    private System.Windows.Forms.Panel panelCardAdapters;
    private System.Windows.Forms.Label lblCardAdaptersTitle;
    private System.Windows.Forms.Label lblCardAdaptersVal;
    private System.Windows.Forms.Label lblCardAdaptersSub;
    private System.Windows.Forms.GroupBox grpProxyQuickCard;
    private System.Windows.Forms.Label lblDashProxyEngineState;
    private System.Windows.Forms.Button btnDashToggleProxy;
    private System.Windows.Forms.Button btnDashRefreshAdapters;
    private System.Windows.Forms.Label lblDashProxySessions;
    private System.Windows.Forms.Panel panelAdaptersTop;
    private System.Windows.Forms.Label lblAdaptersViewTitle;
    private System.Windows.Forms.Button btnRefreshAdaptersGrid;
    private System.Windows.Forms.Button btnSelectAllAdapters;
    private System.Windows.Forms.DataGridView dgvAdapters;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colActive;
    private System.Windows.Forms.DataGridViewTextBoxColumn colName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colIp;
    private System.Windows.Forms.DataGridViewTextBoxColumn colType;
    private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDownSpeed;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUpSpeed;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSessions;
    private System.Windows.Forms.GroupBox grpProxyConfig;
    private System.Windows.Forms.Label lblProxyPort;
    private System.Windows.Forms.NumericUpDown nudProxyPort;
    private System.Windows.Forms.Label lblProxyStrategy;
    private System.Windows.Forms.ComboBox cmbProxyStrategy;
    private System.Windows.Forms.Label lblProxyStateStatus;
    private System.Windows.Forms.Button btnProxyToggle;
    private System.Windows.Forms.Label lblProxyActiveSessions;
    private System.Windows.Forms.Label lblProxyActiveSessionsVal;
    private System.Windows.Forms.GroupBox grpProxyActiveAdapters;
    private System.Windows.Forms.Label lblProxyRoutingNotice;
    private System.Windows.Forms.GroupBox grpDownloaderConfig;
    private System.Windows.Forms.Label lblDownloadUrl;
    private System.Windows.Forms.TextBox txtDownloadUrl;
    private System.Windows.Forms.Label lblDestPath;
    private System.Windows.Forms.TextBox txtDestPath;
    private System.Windows.Forms.Button btnBrowseDest;
    private System.Windows.Forms.Label lblChunkCount;
    private System.Windows.Forms.NumericUpDown nudChunkCount;
    private System.Windows.Forms.Button btnStartDownload;
    private System.Windows.Forms.Button btnCancelDownload;
    private System.Windows.Forms.GroupBox grpDownloaderChunks;
    private System.Windows.Forms.ProgressBar pbDownloader;
    private System.Windows.Forms.Label lblOverallDownloadProgress;
    private System.Windows.Forms.Label lblOverallDownloadSpeed;
    private System.Windows.Forms.ListView lvChunkStats;
    private System.Windows.Forms.ColumnHeader colChunkId;
    private System.Windows.Forms.ColumnHeader colChunkAdapter;
    private System.Windows.Forms.ColumnHeader colChunkIp;
    private System.Windows.Forms.ColumnHeader colChunkSpeed;
    private System.Windows.Forms.ColumnHeader colChunkStatus;
    private System.Windows.Forms.GroupBox grpGeneralSettings;
    private System.Windows.Forms.CheckBox chkStartWithWindows;
    private System.Windows.Forms.CheckBox chkStartMinimized;
    private System.Windows.Forms.CheckBox chkMinimizeOnClose;
    private System.Windows.Forms.CheckBox chkAutoStartProxy;
    private System.Windows.Forms.Button btnSaveSettings;
    private System.Windows.Forms.GroupBox grpUpdates;
    private System.Windows.Forms.Label lblRepoUrl;
    private System.Windows.Forms.TextBox txtRepoUrl;
    private System.Windows.Forms.Button btnCheckUpdates;
    private System.Windows.Forms.Label lblUpdateStatus;
    private System.Windows.Forms.NotifyIcon notifyIcon;
    private System.Windows.Forms.ContextMenuStrip trayMenu;
    private System.Windows.Forms.ToolStripMenuItem menuItemShow;
    private System.Windows.Forms.ToolStripSeparator menuSeparator1;
    private System.Windows.Forms.ToolStripMenuItem menuItemToggleProxy;
    private System.Windows.Forms.ToolStripSeparator menuSeparator2;
    private System.Windows.Forms.ToolStripMenuItem menuItemExit;
    private System.Windows.Forms.Timer timerSpeedCalculation;
}
