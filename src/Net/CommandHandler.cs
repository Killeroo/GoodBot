using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

using GoodBot.Core;
using GoodBot.Logger;

namespace GoodBot.Net
{
    /// <summary>
    /// Static class for sending and handling Command messages
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// Handle & execute a command
        /// </summary>
        /// <param name="c"></param>
        public static void Handle(Command c) /* take address of command giver to display */
        {
            switch (c.Type)
            {
                /* Populate using .ini file */

                case CommandType.Transfer:
                    Log.Command(c);

                    foreach (string path in c.Files)
                        Console.WriteLine(path);

                    break;

                case CommandType.Synchronize:


                    break;

                case CommandType.RsyncStream:

                default:
                    Log.Command(c);
                    break;
            }
        }
        /// <summary>
        /// Send command to host
        /// </summary>
        /// <param name="c"></param>
        /// <param name="client"></param>
        /// <param name="seqnum"></param>
        public static void Send(Command c, TcpClient client, int seqnum)
        {
            try
            {
                // Check if connected first
                if (client == null)
                    throw new Exception();

                byte[] buffer = new byte[GoodBot.Core.Constants.COMMAND_BUFFER_SIZE];
                c.Seq = seqnum;
                c.Destination = IPAddress.Parse(client.Client.RemoteEndPoint.ToString().Split(':')[0]); // Set destination of command
                buffer = Helper.ToByteArray<Command>(c);
                Log.Command(c);
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
            catch (IOException e)
            {
                Log.Error(new Error(e, "Failed to send command"));
            }
            catch (ObjectDisposedException e)
            {
                Log.Error(new Error(e, "Object failure"));
            }
            catch (Exception e)
            {
                Log.Error(new Error(e, "Could not send command"));
            }
        }
    }
}