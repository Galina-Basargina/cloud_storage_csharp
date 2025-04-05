using System;
using System.Data;
using Npgsql;
using Renci.SshNet;

namespace coursework3
{
    internal class DbConnection
    {
        private static NpgsqlConnection connection = null;
        private static SshClient sshClient = null;
        private static ForwardedPortLocal sshPort = null;

        public static NpgsqlConnection getConnection()
        {
            return connection;
        }

        public static NpgsqlConnection connect()
        {
            if (connection != null)
            {
                throw new Exception("Повторное подключение");
            }

            uint db_port = Properties.Settings.Default.db_port;
            string db_name = Properties.Settings.Default.db_name;
            string db_user = Properties.Settings.Default.db_user;
            string db_password = Properties.Settings.Default.db_password;

            string ssh_host = Properties.Settings.Default.ssh_host;
            if (ssh_host == "")
            {
                string db_host = Properties.Settings.Default.db_host;

                string connString = $"Server={db_host};Database={db_name};Port={db_port};User Id={db_user};Password={db_password};";
                connection = new NpgsqlConnection(connString);
            }
            else
            {
                uint ssh_port = Properties.Settings.Default.ssh_port;
                string ssh_user = Properties.Settings.Default.ssh_user;
                string ssh_privatekey = Properties.Settings.Default.ssh_privatekey;
                string ssh_passphrase = Properties.Settings.Default.ssh_passphrase;
                string db_host = "127.0.0.1";

                sshClient = new SshClient(ssh_host, (int)ssh_port, ssh_user, new PrivateKeyFile(ssh_privatekey, ssh_passphrase));
                sshClient.Connect();
                if (!sshClient.IsConnected) return null;

                sshPort = new ForwardedPortLocal(db_host, db_host, db_port);
                sshClient.AddForwardedPort(sshPort);
                sshPort.Start();

                string connString = $"Server={sshPort.BoundHost};Database={db_name};Port={sshPort.BoundPort};User Id={db_user};Password={db_password};";
                connection = new NpgsqlConnection(connString);
            }
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT version();", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("p_out", DbType.String) { Direction = ParameterDirection.Output });
                    command.ExecuteNonQuery();
                    Console.WriteLine(command.Parameters[0].Value);
                }
                return connection;
            }
            catch (NpgsqlException)
            {
                connection = null;
                return null;
            }
        }

        public static void disconnect()
        {
            if (connection != null) 
                connection = null;
            if (sshPort != null)
                sshPort = null;
            if (sshClient != null)
                sshClient = null;
        }
    }
}
