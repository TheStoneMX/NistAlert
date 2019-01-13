using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using BioTechSysNistAlert.Properties;
using BioTechSysNistAlert.Forms;
using BioTechSys.AnsiNist;
using BioTechSys.Mail.Pop3;
using BioTechSys.Biometrics;
using BioTechSysNistAlert.HelperCode;
using System.Collections.ObjectModel;
using Infragistics.Win.UltraWinEditors;


namespace BioTechSysNistAlert
{
    public partial class MainEmailForm : Form
    {
        #region POP3 Variable Declarations
        private ArrayList _attachments;
        private ArrayList _popAttachmentsIndex;
        private bool _lock = false;
        private Pop3Client _pop = null;
        private string _email = "";
        private int _msg_id = 0;
        private ArrayList _attached_file_names;
        private Collection<dbEmail> _Emails;
        private System.Windows.Forms.Timer _mailTimer;
        private NLExtractor _extractor;


        #endregion

        #region ANSI/NIST Variable Declaracionts
        // type 1 Fields
        private string _type1TOT = string.Empty;
        private string _type1Date = string.Empty;
        private string _type1ORI = string.Empty;
        private string _type1TCN = string.Empty;
        private string _type1TCR = string.Empty;

        // Path to the base directory
        private static string baseDir = System.IO.Directory.GetCurrentDirectory();

        // Path to images
        private string imageDir = "";

        // Nist object to be used for creating the transactions.
        private BioAnsiNist m_NistObj;

        // AwareVerification object for verifying the transactions.
        private AwareVerification m_VerifObj;

        // Original height of the large picture box.
        private static int pictureHeight = 248;

        // Original width of the large picture box.
        private static int pictureWidth = 248;

        // The current picture that is being viewed.
        private int currentPicture = -1;
        // The last finger that was being edited.
        private int lastFinger = -1;

        // Stores the images that are being displayed.
        private Image[] type4Images = new System.Drawing.Image[14];
        private Image[] type14Images = new System.Drawing.Image[3];

        //Stores whether or not we are currently loading a file.
        private bool loading = false;

        //Stores whether or not a fingerprint image is missing.
        bool[] missingImage = new bool[10];

        //Stores whether or not a certain tab contains an error.
        bool[] tabError = new bool[4];

        #region Constant Declarations
        const int type1RecordIndex = 0;
        const int type2RecordIndex = 0;
        const string VerifierFile = "ANSI_NIST_MailServer_Verification.txt";
        const int defaultCompression = 10;
        #endregion



        #endregion

        public MainEmailForm()
        {
            InitializeComponent();
            InitPop3Objects();
            initializeAnsiNistObjects();
            IniGetMailTimer();
        }
        public void InitPop3Objects()
        {
            _pop = new Pop3Client();
            _attachments = new ArrayList();
            _popAttachmentsIndex = new ArrayList();
            _attached_file_names = new ArrayList();
        }
        private void IniGetMailTimer()
        {
            _mailTimer = new System.Windows.Forms.Timer();
        }
        private void initializeAnsiNistObjects()
        {
            try
            {
                if (m_NistObj == null)
                {
                    m_NistObj = new BioAnsiNist();
                    m_NistObj.ThrowExceptionOnError = true;
                }
                //Load the verification structure. We need to read the verification file to use
                //the set and get function calls. The verification file holds information on how
                //to translate a mnemonic (for example “TOT”, “ORI” and “DAT” ) to record,
                //field, subfield, and item numbers. 
                if (m_VerifObj == null)
                    m_VerifObj = new AwareVerification();
                //
                m_VerifObj.readVerification(VerifierFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "BioTechSys");
                UpdateStatusBar( ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //You can set these values directly in Design mode.
            Settings settings = Settings.Default;
            _pop.Pop3Server = settings.snsp_popMail;
            _pop.UserName = settings.snsp_userName;
            _pop.Password = settings.snsp_password;
            _pop.Pop3Port = Convert.ToInt16(settings.snsp_Pop3Port);

            //
            toolStripDisconnect.Enabled = false;
            toolStripGetMail.Enabled = false;
            toolStripDisconnect.Checked = true;

            _mailTimer.Tick += new System.EventHandler(ElapsedEventHandler);
            _mailTimer.Interval = settings.MailTimer;
            
            //
            FillListViewDBemails();
        }
        public void ElapsedEventHandler(object source, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(this.ReceiveEmails));
            th.Start();

        }
        private void FillListViewDBemails()
        {
            // Lets get the emails first from DB
            _Emails = DBHelper.GetEmailsFromDB();
            
            // now lets fill the ListView with DB Emails.
            for (int i = 0; i < _Emails.Count; i++)
            {
                FillInboxListView(_Emails[i], i );
            }
        }
        private void FillInboxListView( dbEmail _Emails, int counter )
        {
            DateTime date_time;
            string temp = "";

            date_time = DateTime.Parse(_Emails.Date);
            temp = date_time.ToString("U");

            ListViewItem item = new ListViewItem();
            item.UseItemStyleForSubItems = false;

            item.Text = _Emails.DE;
            item.SubItems.Add(_Emails.PARA);
            item.SubItems.Add(_Emails.Subject);
            item.SubItems.Add(temp);
            item.SubItems.Add(_Emails.TOT);
            item.SubItems.Add(_Emails.NCP);
            item.SubItems.Add(_Emails.Status.ToString());
            
            if (_Emails.Status.ToString() == "NO HIT")
            {
                item.SubItems[6].BackColor = Color.Green; 
            }
            else if (_Emails.Status.ToString() == "HIT")
                item.SubItems[6].BackColor = Color.Red; 


            this.InsertItem(item);
        }
        private void sNSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NLExtractor.IsRegistered)
            {
                POP3SnspPropForm form = new POP3SnspPropForm();
                form.ShowDialog();

                Settings settings = Settings.Default;
                _pop.Pop3Server = settings.snsp_popMail;
                _pop.UserName = settings.snsp_userName;
                _pop.Password = settings.snsp_password;
                _pop.Pop3Port = Convert.ToInt16(settings.snsp_Pop3Port);
            }            
            else
                    MessageBox.Show("Uno de los componentes de BioTechSysNistAlert no esta activado", 
                                    "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error );

        }
        private void toolStripGetMail_Click(object sender, EventArgs e)
        {
            if (NLExtractor.IsRegistered)
            {

                ToolStripButton button = (ToolStripButton)sender;
                if (button.Checked == true)
                    _mailTimer.Start();
                else
                    if (button.Checked == false)
                        _mailTimer.Stop();
            }
            else
                MessageBox.Show("Uno de los componentes de BioTechSysNistAlert no esta activado.",
                                "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void toolStripConnect_Click(object sender, EventArgs e)
        {
            if (NLExtractor.IsRegistered)
            {

                if (CheckInputValidationForPop(_pop.Pop3Port.ToString()))
                {
                    if (Internet.IsConnectedToInternet())
                    {
                        Thread th = new Thread(new ThreadStart(this.ReceiveEmails));
                        th.Start();
                    }
                    else
                    {
                        MessageBox.Show(this, "Usted no esta conectado al Internet.", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
                MessageBox.Show("Uno de los componentes de BioTechSysNistAlert no esta activado.", 
                                "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error );

        }
        private void toolStripDisconnect_Click(object sender, EventArgs e)
        {
            if (_pop != null)
            {
                    try
                    {
                        _pop.Disconnect();
                        this.EnableDisableConnectButton(true);
                        this.EnableDisableDisconnectButton(false);
                        toolStripGetMail.Enabled = false;
                   }
                    catch (Pop3ClientException err)
                    {
                        MessageBox.Show(this, err.ErrorMessage, "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            } 
            
        }
        private void MailMessages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.MailMessages.SelectedItems.Count > 0)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                Image image = (Image)BioTechSysNistAlert.Properties.Resources.delete;
                menu.Items.Add("Delete", image);
                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemDeleteClicked);
                menu.Show(Control.MousePosition);
            }
            else if (e.Button == MouseButtons.Left && this.MailMessages.SelectedItems.Count > 0)
            {
                int index = this.MailMessages.SelectedItems[0].Index;
                index = index + 1;
                
                // cleanup all the TextBoxes Info
                this._email = "";
                this._msg_id = index;
                this._attached_file_names.Clear();
                this._popAttachmentsIndex.Clear();
                
                // Get all the mailInfo and display it
                FetchEmailNistTypeInfo(index);
            }
        }
        private void MailMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(this.MailMessages.SelectedItems.Count > 0))
            {
                clearAllTextBoxes();
            }
        }
        public void clearAllTextBoxes()
        {
            textBoxT1_TOT.Text = string.Empty;
            textBoxT1_DAT.Text = string.Empty;
            textBoxT1_DAI.Text = string.Empty;
            textBoxT1_ORI.Text = string.Empty;
            textBoxT1_TCN.Text = string.Empty;
            textBoxT1_TCR.Text = string.Empty;
            txtT2_CNO.Text = string.Empty;
            txtT2_CRN.Text = string.Empty;
            txtT2_ORN.Text = string.Empty;
            txtT2_RES.Text = string.Empty;
            txtT2_HNA.Text = string.Empty;
            txtT2_IPA.Text = string.Empty;

            txtT2_HFN.Text = string.Empty;
            txtT2_HDB.Text = string.Empty;
            txtT2_HSE.Text = string.Empty;
            txtT2_HPT.Text = string.Empty;
            txtT2_HST.Text = string.Empty;
            txtT2_ULR.Text = string.Empty;
            cbxMAP.Items.Clear();

        }
        private void menu_ItemDeleteClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Realmente quiere Borrar este correo?", "BioTechSys", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = this.MailMessages.SelectedItems[0].Index;
                index = index + 1;
                try
                {
                    //_pop.DeleteEmail(index);
                    MailMessages.SelectedItems[0].Remove();
                    //
                    // Now Delete from Data Base
                    DBHelper.DeleteEmail(_Emails[index - 1].dbIndex);

                }
                catch (Pop3ClientException err)
                {
                    MessageBox.Show(this, err.ErrorMessage, "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool CheckInputValidationForPop( string pop_port )
        {
            if (_pop.Pop3Server.Equals(""))
            {
                MessageBox.Show(this, "You must provide pop server address.", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (pop_port.Equals(""))
            {
                MessageBox.Show(this, "You must provide pop port number.", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (_pop.UserName.Equals(""))
            {
                MessageBox.Show(this, "You must provide username.", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (_pop.Password.Equals(""))
            {
                MessageBox.Show(this, "You must provide password.", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

       // The delegate used to enable or disable a button control, in another thread
        private delegate void EnableDisableEventHandler(bool enable);

        private delegate void UpdateStatusBarEventHandler(string text);

        private void UpdateStatusBar(string text)
        {
            if (this.statusStrip.InvokeRequired)
            {
                UpdateStatusBarEventHandler obj = new UpdateStatusBarEventHandler(this.UpdateStatusBar);
                this.Invoke(obj, new object[] { text });
            }
            else
            {
                this.ProgressLabel.Text = text;
            }
        }
        private void EnableDisableConnectButton(bool enable)
        {
            if (this.toolStrip.InvokeRequired)
            {
                EnableDisableEventHandler obj = new EnableDisableEventHandler(this.EnableDisableConnectButton);
                this.Invoke(obj, new object[] { enable });
            }
            else
            {
                this.toolStripConnect.Enabled = enable;
            }
        }
        private void EnableDisableDisconnectButton(bool enable)
        {
            if (this.toolStrip.InvokeRequired)
            {
                EnableDisableEventHandler obj = new EnableDisableEventHandler(this.EnableDisableDisconnectButton);
                this.Invoke(obj, new object[] { enable });
            }
            else
            {
                toolStripDisconnect.Enabled = enable;
                toolStripGetMail.Enabled = true;

            }
        }

        #region Helping methods for Pop3 Client user Interface

        private void ReceiveEmails()
        {
            try
            {
                EnableDisableConnectButton(false);

                _pop.ConnectionEstablishing += new ConnectEventHandler(this.pop_ConnectionEstablishing);
                _pop.ConnectionEstablished += new ConnectEventHandler(this.pop_ConnectionEstablished);
                _pop.AuthenticationBegan += new AuthenticateEventHandler(this.pop_AuthenticationBegan);
                _pop.AuthenticationFinished += new AuthenticateEventHandler(this.pop_AuthenticationFinished);
                _pop.StartedDataReceiving += new DataReceivingEventHandler(this.pop_StartedDataReceiving);
                _pop.EndedDataReceiving += new DataReceivingEventHandler(this.pop_EndedDataReceiving);
                _pop.Disconnected += new DisconnectEventHandler(this.pop_Disconnected);

                _pop.Connect();

                this.EnableDisableDisconnectButton(true);

                _pop.GetMailBoxDetails();   //it sets the TotalEmails and TotalEmailSize properties

                if (_pop.TotalEmails >= 1)
                {
                    this.UpdateStatusBar("Reciviendo Emails");

                    for (int i = 1; i < _pop.TotalEmails + 1; i++)
                    {
                        FetchEmails(i);
                    }
                    // no disconect from the Server
                    _pop.Disconnect();
                    this.UpdateStatusBar("Se termino de recivir Emails");
                }
                else
                {
                    this.UpdateStatusBar("No hay mensages de email en el servidor... - BioTechSysNistAlert");
                }
            }
            catch (SmtpClientException err)
            {
                MessageBox.Show(this, err.ErrorMessage, "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.UpdateStatusBar("BioTechSysNistAlert");
                this.EnableDisableConnectButton(true);
                this.EnableDisableDisconnectButton(false);
            }
        }
        private void FetchEmails(int index)
        {
            try
            {
                pop_CursorWaitBegan(this, _pop.UserName);

                _email = _pop.FetchEmail(index);

                string content = "";
                string content_type = "";
                bool isHtmlIncluded = false;
                string attached_file_name = "";
                int plain_text_message_section = -1;

                for (int i = 1; i <= this._pop.MailSections; i++)
                {
                    this._pop.GetMailSection(i, ref content, ref content_type, ref attached_file_name);

                    if (content_type.ToLower().Equals("text/html"))
                    {
                        this.WritePopMessage(content);
                        isHtmlIncluded = true;
                    }
                    else 
                        if (content_type.ToLower().Equals("base64"))
                    {
                        ListViewItem item = new ListViewItem(attached_file_name);
                        item.ImageIndex = 0;
                        _attached_file_names.Add(item);
                        _popAttachmentsIndex.Add(i);
                    }
                    else if (content_type.ToLower().Equals("text/plain"))
                    {
                        plain_text_message_section = i;
                    }
                }

                if (_attached_file_names.Count == 0)
                {
                    pop_CursorWaitFinished(this, _pop.UserName);
                    return;
                }

                byte[] decoded_data = MailDecoder.ConvertFromBase64String(content);

                if (decoded_data == null)
                {
                    pop_CursorWaitFinished(this, _pop.UserName);
                    return;
                }

                // Here we get the AnsiNisit File Info
                m_NistObj.readTransactionMem(decoded_data, m_VerifObj);
                string memonics = string.Empty; string TCR = string.Empty; string TOT = string.Empty; string Status = string.Empty;

                TOT = getMnemonic("T1_TOT" ); // Type of Transaction
                TCR = getMnemonic("T1_TCR" );// Numero de transaccion

                // Lets get the Status of "NO HIT" or "HIT"
                if (TOT == "SRE")
                    Status = getMnemonic( "T2_RES" ); // Type of Transaction

                // here we insert all the info in the DB
                Int64 dbIndex = DBHelper.InsertEmailInfo(_pop, attached_file_name, decoded_data, TOT, TCR, Status, _email);

                if (dbIndex == -1)
                {
                    MessageBox.Show("Error Inserttando el Objeto de Email en La Base de Datos", "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // now we populate ListView with the new email
                FillInboxListView( _pop, TOT, TCR, Status );
                // now insert the email in the Email Collection
                _Emails.Add(new dbEmail( _pop.From, _pop.To, _pop.Subject, _pop.Date, 
                                         decoded_data, attached_file_name, 
                                         TOT, TCR, Status, dbIndex ));

                // we saved the EMail in the DB now delete from Server...
                _pop.DeleteEmail(index);

            }
            catch (Pop3ClientException err)
            {
                MessageBox.Show(this, err.ErrorMessage, "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pop_CursorWaitFinished(this, _pop.UserName);
            }
            pop_CursorWaitFinished(this, _pop.UserName);

        }
        // The delegate used to write Pop Message in browser
        private delegate void WritePopMessageEventHandler(string content);
        private void WritePopMessage(string content)
        {
            if (this.PopMessage.InvokeRequired)
            {
                WritePopMessageEventHandler obj = new WritePopMessageEventHandler(this.WritePopMessage);
                this.Invoke(obj, new object[] { content });
            }
            else
            {
                this.PopMessage.DocumentText = content.Trim();
            }
        }
        private void FillInboxListView(Pop3Client obj, string TOT, string TCR, string Status)
        {
            DateTime date_time;
            string temp = "";

            date_time = DateTime.Now; // Parse(obj.Date);
            temp = date_time.ToString("U");

            ListViewItem item = new ListViewItem();
            item.UseItemStyleForSubItems = false;

            item.Text = obj.From;
            item.SubItems.Add(obj.To);
            item.SubItems.Add(obj.Subject);
            item.SubItems.Add(temp);
            item.SubItems.Add(TOT);
            item.SubItems.Add(TCR);

            if (Status != string.Empty)
                item.SubItems.Add(Status);

            if (Status == "NO HIT")
            {
                item.SubItems[6].BackColor = Color.Green;
            }
            else if (Status == "HIT")
                item.SubItems[6].BackColor = Color.Red; 

            this.InsertItem(item);
        }

        #region Event Handlers
        private void pop_Disconnected(object sender, string Server)
        {
            if (this.statusStrip.InvokeRequired)
            {
                DisconnectEventHandler discon = new DisconnectEventHandler(this.pop_Disconnected);
                this.Invoke(discon, new object[] { sender, Server });
            }
            else
            {
                this.ProgressLabel.Text = "Disconectandose del servidor \"" + Server + "\"";
                Thread.Sleep(500);
                this.ProgressLabel.Text = "BioTechSysNistAlert";
            }
        }
        private void pop_EndedDataReceiving(object sender)
        {
            if (this.statusStrip.InvokeRequired)
            {
                DataReceivingEventHandler data = new DataReceivingEventHandler(this.pop_EndedDataReceiving);
                this.Invoke(data, new object[] { sender });
            }
            else
            {
                this.ProgressLabel.Text = "El mensage de Email se a recivido";
            }
        }
        private void pop_StartedDataReceiving(object sender)
        {
            if (this.statusStrip.InvokeRequired)
            {
                DataReceivingEventHandler data = new DataReceivingEventHandler(this.pop_StartedDataReceiving);
                this.Invoke(data, new object[] { sender });
            }
            else
            {
                this.ProgressLabel.Text = "Reciviendo mensages de email";
            }
        }
        private void pop_ConnectionEstablishing(object sender, string Server, int Port)
        {
            if (this.statusStrip.InvokeRequired)
            {
                ConnectEventHandler con = new ConnectEventHandler(this.pop_ConnectionEstablishing);
                this.Invoke(con, new object[] { sender, Server, Port });
            }
            else
            {
                this.ProgressLabel.Text = "Conectándose al Servidor  \"" + Server + "\" on port " + Port;
            }
        }
        private void pop_ConnectionEstablished(object sender, string Server, int Port)
        {
            if (this.statusStrip.InvokeRequired)
            {
                ConnectEventHandler con = new ConnectEventHandler(this.pop_ConnectionEstablished);
                this.Invoke(con, new object[] { sender, Server, Port });
            }
            else
            {
                this.ProgressLabel.Text = "La Conexión con el Servidor fue Exitosa\"" + Server + "\"";
            }
        }
        private void pop_AuthenticationFinished(object sender, string userName)
        {
            if (this.statusStrip.InvokeRequired)
            {
                AuthenticateEventHandler auth = new AuthenticateEventHandler(this.pop_AuthenticationFinished);
                this.Invoke(auth, new object[] { sender, userName });
            }
            else
            {
                this.ProgressLabel.Text = "Verificación completada";
            }
        }
        private void pop_AuthenticationBegan(object sender, string userName)
        {
            if (this.statusStrip.InvokeRequired)
            {
                AuthenticateEventHandler auth = new AuthenticateEventHandler(this.pop_AuthenticationBegan);
                this.Invoke(auth, new object[] { sender, userName });
            }
            else
            {
                this.ProgressLabel.Text = "Verificando Nombre de Usuario y contraseña";
            }
        }
        private void pop_CursorWaitBegan(object sender, string userName)
        {
            if (this.InvokeRequired)
            {
                AuthenticateEventHandler auth = new AuthenticateEventHandler(this.pop_CursorWaitBegan);
                this.Invoke(auth, new object[] { sender, userName });
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }
        }
        private void pop_CursorWaitFinished(object sender, string userName)
        {
            if (this.InvokeRequired)
            {
                AuthenticateEventHandler auth = new AuthenticateEventHandler(this.pop_CursorWaitFinished);
                this.Invoke(auth, new object[] { sender, userName });
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void FetchEmailNistTypeInfo(int index )
        {
            try
            {
                pop_CursorWaitBegan(this, _pop.UserName);
                //
                byte[] decoded_data = _Emails[index-1].Attachment;
                // Here we get the AnsiNisit File Info
                m_NistObj.readTransactionMem(decoded_data, m_VerifObj);
                //
                SetType1TxtBoxInfo();
                //
                SetType2TxtBoxInfo();
                
                this.UpdateStatusBar("BioTechSysNistAlert");

            }
            catch (Pop3ClientException err)
            {
                MessageBox.Show(this, err.ErrorMessage, "BioTechSysNistAlert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pop_CursorWaitFinished(this, _pop.UserName);
            }
            pop_CursorWaitFinished(this, _pop.UserName);

        }
        private void SetType1TxtBoxInfo()
        {
            setTextBox("T1_LEN", textBoxT1_TOT);
            setTextBox("T1_TOT", textBoxT1_TOT);
            setTextBox("T1_DAT", textBoxT1_DAT);
            setTextBox("T1_DAI", textBoxT1_DAI);
            setTextBox("T1_ORI", textBoxT1_ORI);
            setTextBox("T1_TCN", textBoxT1_TCN);
            setTextBox("T1_TCR", textBoxT1_TCR);
        }
        #region SetType2TxtBoxInfo
        private void SetType2TxtBoxInfo()
        {
            setTextBox("T2_CNO", txtT2_CNO );
            setTextBox("T2_CRN", txtT2_CRN );
            setTextBox("T2_ORN", txtT2_ORN );
            setTextBox("T2_RES", txtT2_RES );

            int counter = 2;
            string map = getMnemonic("T2_MAP");
          
            if (map != null)
	        {
                cbxMAP.Items.Add(map);
                do
                {
                    map = getMnemonic("T2_MAP" + counter.ToString());
                    if (map != null)
                    {
                        cbxMAP.Items.Add(map);
                        counter++;
                        map = string.Empty;
                    }

                } while (map != null);
	        }
            //
            cbxMAP.SelectedIndex = 0;
                        
            setTextBox("T2_HNA", txtT2_HNA );  // Apellido paterno de la persona coincidente
            setTextBox("T2_IPA", txtT2_IPA );  // Apellido materno de la persona coincidente
            setTextBox("T2_HFN", txtT2_HFN );  // Nombre´s de la persona coincidente
            
            setTextBox("T2_HDB", txtT2_HDB );  // Fecha de Nacimiento de la persona coincidente
            
            // "Sexo del candidato"
            string sex = getMnemonic("T2_HSE");
            if (sex == "M")
                txtT2_HSE.Text = "Masculinno";
            else if (sex == "F")
                txtT2_HSE.Text = "Femenino";
            
            #region  Codigo de tipo de registro de la persona coincidente"
            switch (getMnemonic("T2_HPT"))
            {
                case "CRI":
                    txtT2_HPT.Text = "Criminal";
                    break;
                case "POL":
                    txtT2_HPT.Text = "Policia";
                    break;
                case "USU":
                    txtT2_HPT.Text = "Usuario";
                    break;
                case "ADM":
                    txtT2_HPT.Text = "Administrativo";
                    break;
                case "EXT":
                    txtT2_HPT.Text = "Externo";
                    break;
                case "IDE":
                    txtT2_HPT.Text = "Identificado";
                    break;
                case "DES":
                    txtT2_HPT.Text = "Desconocido";
                    break;
                default:
                    break;
            }
            #endregion

            #region Codigo de situacion de la persona coincidente"
            switch (getMnemonic("T2_HST"))
            {
                case "OTR":
                    txtT2_HST.Text = "Otro";
                    break;
                case "LIB":
                    txtT2_HST.Text = "Liberado";
                    break;
                case "INT":
                    txtT2_HST.Text = "Interno";
                    break;
                case "FAL":
                    txtT2_HST.Text = "Fallecido";
                    break;
                case "BUS":
                    txtT2_HST.Text = "Buscado";
                    break;
                default:
                    break;
            }
            #endregion

            setTextBox("T2_ULR", txtT2_ULR );
        }
        #endregion
        private void setTextBox(string mnemonic, UltraTextEditor textBox)
        {
            try
            {
                textBox.Text = m_NistObj.get(mnemonic, 1, 1);
            }
            catch (Exception ex) { UpdateStatusBar(ex.Message); }
        }
        private string getMnemonic( string mnemonic )
        {
            try
            {
                return m_NistObj.get(mnemonic, 1, 1);
            }
            catch (Exception ex) {UpdateStatusBar(ex.Message); }
            return null;


        }    
        
        #endregion
        // The delegate used to insert an item in the Inbox ListView
        private delegate void InboxItemEventHandler(ListViewItem item);
        private void InsertItem(ListViewItem item)
        {
            if (this.MailMessages.InvokeRequired)
            {
                InboxItemEventHandler obj = new InboxItemEventHandler(this.InsertItem);
                this.Invoke(obj, new object[] { item });
            }
            else
            {
                this.MailMessages.Items.Add(item);
            }
        }

        private void EmailTimer_Tick(object sender, EventArgs e)
        {
            toolStripDisconnect_Click(this, e);
        }
    }
}
