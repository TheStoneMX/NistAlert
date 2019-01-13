using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BioTechSysNistAlert.HelperCode
{
    class dbEmail
    {
        public dbEmail()
        {
            
        }
        public dbEmail(string dE, string pARA, string subject, 
                        string date, 
                        byte[]  attachment, 
                        string attachmentName, 
                        string tOT, string nCP, string status, Int64 Index )
        {
            _DE = dE;
            _PARA = pARA;
            _Subject = subject;
            _Date = date;
            _Attachment = attachment;
            _AttachmentName = attachmentName;
            _TOT = tOT;
            _NCP = nCP;
            _Status = status;
            _DbIndex = Index;
        }
        private Int64 _DbIndex;
        public Int64 dbIndex
        {
            get { return _DbIndex; }
            set { _DbIndex = value; }
        }
        private string _DE;
        public string DE
        {
            get { return _DE; }
            set { _DE = value; }
        }
        private string  _PARA;
        public string  PARA
        {
            get { return _PARA; }
            set { _PARA = value; }
        }
        private string _Subject;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        private string _AttachmentName;
        public string AttachmentName
        {
            get { return _AttachmentName; }
            set { _AttachmentName = value; }
        }
        private byte[] _Attachment;
        public byte[] Attachment
        {
            get { return _Attachment; }
            set { _Attachment = value; }
        }
        private string _Date;
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _TOT;
        public string TOT
        {
            get { return _TOT; }
            set { _TOT = value; }
        }
        private string _NCP;
        public string NCP
        {
            get { return _NCP; }
            set { _NCP = value; }
        }
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
