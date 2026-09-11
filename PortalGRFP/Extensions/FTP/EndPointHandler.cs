using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using WinSCP;

namespace PortalGRFP.Extensions.FTP
{
    public class EndPointHandler
    {

        #region Propiedades de la clase

        private EndPointType nType = EndPointType.Undefined;
        private String sServer = String.Empty;
        private int nPort = 0;
        private EndPointAuthentication nAuthentication = EndPointAuthentication.Default;
        private String sUsername = String.Empty;
        private String sPassword = String.Empty;
        private String sPath = String.Empty;

        public EndPointType Type
        {
            get { return nType; }
            set { nType = value; }
        }

        public String Server
        {
            get { return sServer; }
            set { sServer = value; }
        }

        public int Port
        {
            get { return nPort; }
            set { nPort = value; }
        }

        public EndPointAuthentication Authentication
        {
            get { return nAuthentication; }
            set { nAuthentication = value; }
        }

        public String Username
        {
            get { return sUsername; }
            set { sUsername = value; }
        }

        public String Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        public String Path
        {
            get { return sPath; }
            set { sPath = value; }
        }

        #endregion

        #region Veriables y objetos de uso exclusivo de la clase

        private SessionOptions objRemoteOptions = null;
        private CredentialCache objNetCache = null;

        #endregion

        #region Constructor de la clase

        public EndPointHandler()
        {
            // Do nothing
        }

        #endregion

        #region Metodos de la clase

        public List<EndPointFileHandler> GetFileList()
        {
            try
            {
                List<EndPointFileHandler> objList = new List<EndPointFileHandler>();

                // Login
                DoAuthenticate();

                // Ejecuta la lectura remota
                if (nType == EndPointType.FTP || nType == EndPointType.SFTP)
                {
                    using (Session objSession = new Session())
                    {
                        // Abre la conexion al servidor
                        objSession.Open(objRemoteOptions);

                        // Consulta la lista de archivos
                        RemoteDirectoryInfo objRDir = objSession.ListDirectory(sPath);

                        objList = (from x in objRDir.Files
                                   where x.IsDirectory == false
                                   select new EndPointFileHandler()
                                   {
                                       Name = x.Name,
                                       Fullname = x.FullName,
                                       Size = x.Length,
                                   }).ToList();
                    }
                }

                // Ejecuta la lectura local
                if (nType == EndPointType.Local || nType == EndPointType.Network)
                {
                    // Construye la referencia a la carpeta
                    DirectoryInfo objLDir = null;

                    if (nType == EndPointType.Network)
                    {
                        objLDir = new DirectoryInfo(System.IO.Path.Combine(@"\\" + sServer, sPath));
                    }
                    else
                    {
                        objLDir = new DirectoryInfo(sPath);
                    }

                    // Consulta la lista de archivos
                    objList = (from x in objLDir.GetFiles()
                               select new EndPointFileHandler()
                               {
                                   Name = x.Name,
                                   Fullname = x.FullName,
                                   Size = x.Length,
                               }).ToList();
                }

                return objList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean GetFile(String SourceFile, String TargetFile)
        {
            try
            {
                Boolean boolComplete = false;

                // Login
                DoAuthenticate();

                // Ejecuta la lectura remota
                if (nType == EndPointType.FTP || nType == EndPointType.SFTP)
                {
                    using (Session objSession = new Session())
                    {
                        // Abre la conexion al servidor
                        objSession.Open(objRemoteOptions);

                        // Descarga los archivos
                        TransferOptions objOptions = new TransferOptions()
                        {
                            TransferMode = TransferMode.Binary,
                        };

                        TransferOperationResult objResult = objSession.GetFiles(SourceFile, TargetFile, false, objOptions);

                        // Lanza cualquier excepcion
                        objResult.Check();

                        // Finaliza la operacion
                        boolComplete = true;
                    }
                }

                // Ejecuta la lectura local
                if (nType == EndPointType.Local || nType == EndPointType.Network)
                {
                    // Crea la referencia al archivo
                    FileInfo objFile = new FileInfo(SourceFile);

                    // Copia el archivo al destino especificado
                    objFile.CopyTo(TargetFile);

                    // Finaliza la operacion
                    boolComplete = true;
                }

                return boolComplete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean PutFile(String SourceFile, String TargetFile)
        {
            try
            {
                Boolean boolComplete = false;

                // Login
                DoAuthenticate();

                // Ejecuta la lectura remota
                if (nType == EndPointType.FTP || nType == EndPointType.SFTP)
                {
                    using (Session objSession = new Session())
                    {
                        // Abre la conexion al servidor
                        objSession.Open(objRemoteOptions);

                        // Descarga los archivos
                        TransferOptions objOptions = new TransferOptions()
                        {
                            TransferMode = TransferMode.Binary,
                        };

                        TransferOperationResult objResult = objSession.PutFiles(SourceFile, TargetFile, false, objOptions);

                        // Lanza cualquier excepcion
                        objResult.Check();

                        // Finaliza la operacion
                        boolComplete = true;
                    }
                }

                // Ejecuta la lectura local
                if (nType == EndPointType.Local || nType == EndPointType.Network)
                {
                    // Crea la referencia al archivo
                    FileInfo objFile = new FileInfo(SourceFile);

                    // Copia el archivo al destino especificado
                    objFile.CopyTo(TargetFile);

                    // Finaliza la operacion
                    boolComplete = true;
                }

                return boolComplete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean DeleteFile(String SourceFile)
        {
            try
            {
                Boolean boolComplete = false;

                // Login
                DoAuthenticate();

                // Ejecuta la lectura remota
                if (nType == EndPointType.FTP || nType == EndPointType.SFTP)
                {
                    using (Session objSession = new Session())
                    {
                        // Abre la conexion al servidor
                        objSession.Open(objRemoteOptions);

                        // Elimina el archivo
                        RemovalOperationResult objResult = objSession.RemoveFiles(SourceFile);

                        // Lanza cualquier excepcion
                        objResult.Check();

                        // Finaliza la operacion
                        boolComplete = true;
                    }
                }

                // Ejecuta la lectura local
                if (nType == EndPointType.Local || nType == EndPointType.Network)
                {
                    // Crea la referencia al archivo
                    FileInfo objFile = new FileInfo(SourceFile);

                    // Copia el archivo al destino especificado
                    objFile.Delete();

                    // Finaliza la operacion
                    boolComplete = true;
                }

                return boolComplete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String BuildBackupName(String SourceFile)
        {
            try
            {
                String strFile = String.Empty;

                if (nType == EndPointType.FTP || nType == EndPointType.SFTP)
                {
                    if (sPath.Substring(sPath.Length - 1) == @"/")
                    {
                        strFile = sPath + SourceFile;
                    }
                    else
                    {
                        strFile = sPath + @"/" + SourceFile;
                    }
                }

                // Ejecuta la lectura local
                if (nType == EndPointType.Local || nType == EndPointType.Network)
                {
                    if (sPath.Substring(sPath.Length - 1) == @"\")
                    {
                        strFile = sPath + SourceFile;
                    }
                    else
                    {
                        strFile = sPath + @"\" + SourceFile;
                    }
                }

                return strFile;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        #endregion

        #region Funciones de uso exclusivo de la clase

        private void DoAuthenticate()
        {
            switch (nType)
            {
                case EndPointType.FTP:
                    AuthenticateFTP();

                    break;
                case EndPointType.SFTP:
                    AuthenticateSFTP();

                    break;
                case EndPointType.Local:
                    // Do nothing

                    break;
                case EndPointType.Network:
                    AuthenticateNetwork();

                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        private void AuthenticateFTP()
        {
            switch (nAuthentication)
            {
                case EndPointAuthentication.Basic:
                    objRemoteOptions = new SessionOptions
                    {
                        Protocol = Protocol.Ftp,
                        HostName = sServer,
                        PortNumber = nPort,
                        UserName = sUsername,
                        Password = sPassword,
                    };

                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        private void AuthenticateSFTP()
        {
            switch (nAuthentication)
            {
                case EndPointAuthentication.Basic:
                    objRemoteOptions = new SessionOptions
                    {
                        Protocol = Protocol.Sftp,
                        HostName = sServer,
                        PortNumber = nPort,
                        UserName = sUsername,
                        Password = sPassword,
                        // GiveUpSecurityAndAcceptAnySshHostKey = true,
                        SshHostKeyPolicy = SshHostKeyPolicy.GiveUpSecurityAndAcceptAny,
                    };

                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        private void AuthenticateNetwork()
        {
            switch (nAuthentication)
            {
                case EndPointAuthentication.Basic:
                    if (objNetCache == null)
                    {
                        NetworkCredential objNetworkCredential = new NetworkCredential(sUsername, sPassword);
                        objNetCache = new CredentialCache();
                        objNetCache.Add(new Uri(@"\\" + sServer), "Basic", objNetworkCredential);
                    }

                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        #endregion

    }

    public enum EndPointType
    {
        Undefined,
        FTP,
        SFTP,
        Local,
        Network
    }

    public enum EndPointAuthentication
    {
        Default,
        Basic
    }

}
