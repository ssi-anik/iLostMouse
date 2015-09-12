using iLostMouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace iLostMouse_Windows
{
    class Server
    {

        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x0010;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        private byte[] buffer;
        private List<Socket> clientSocketList;
        private Socket serverSocket;
        private int port = 11017;
        private Form1 form;
        private bool isServerStopped = false;

        public Server(Form1 form)
        {
            this.form = form;
            buffer = new byte[1024];
            clientSocketList = new List<Socket>();
        }

        private Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private IPEndPoint ipEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, port);
        }

        public void stop_listening()
        {
            isServerStopped = true;
            serverSocket.Dispose();
            show_message("Success: Server stopped successfully.");
        }

        public void start_listening()
        {
            setup_server();
        }

        private void setup_server()
        {
            try
            {
                serverSocket = socket();
                serverSocket.Bind(ipEndPoint());
                serverSocket.Listen(2);
                server_start_accepting_connection();
                show_message("Success: Server started successfully.");
            }
            catch (SocketException)
            {
                show_message("Error: Error occurred while setting up server.");
            }

        }

        private void acceptCallback(IAsyncResult asyncResult)
        {
            if (!isServerStopped)
            {
                show_message("Success: An application is now connected.");
                Socket socket = server_release_accepted_connection(asyncResult);
                clientSocketList.Add(socket);
                socket_start_receiving_request(socket);
                server_start_accepting_connection();
            }

        }

        private void receiveCallback(IAsyncResult asyncResult)
        {
            // exams going, can't handle so many error.
            // if you're checking this one,
            // then hanlde error IndexOutOfBound for the incoming commands :3 

            Socket socket = (Socket)asyncResult.AsyncState;
            int received_buffer_length = socket_end_receiving_request(socket, asyncResult);

            if (received_buffer_length > 0)
            {
                byte[] received_buffer_data = new byte[received_buffer_length];

                Array.Copy(buffer, received_buffer_data, received_buffer_length);
                String received_text = Encoding.ASCII.GetString(received_buffer_data).ToLower();

                String response = String.Empty;
                String message_to_show = received_text;

                String formatted_text = received_text.Trim("\n".ToCharArray());

                String[] commands = formatted_text.Split('|');

                if (commands[0] == "move")
                {
                    String[] positions = commands[1].Split(':');
                    int current_cursor_x_position = Cursor.Position.X;
                    int current_cursor_y_position = Cursor.Position.Y;
                    try
                    {
                        int move_in_x = Int32.Parse(positions[0]);
                        int move_in_y = Int32.Parse(positions[1]);
                        int changed_cursor_x_position = current_cursor_x_position + move_in_x;
                        int chagned_cursor_y_position = current_cursor_y_position + move_in_y;

                        Cursor.Position = new System.Drawing.Point(changed_cursor_x_position, chagned_cursor_y_position);
                        
                        message_to_show = "Success: Moved mouse cursor.";
                        response = "Request accepted.";
                    }
                    catch (Exception)
                    {
                        message_to_show = "Error: Invalid move parameter.";
                        response = "Request denied.";
                    }
                }
                else if (commands[0] == "drag")
                {
                    String[] positions = commands[1].Split(':');
                    int current_cursor_x_position = Cursor.Position.X;
                    int current_cursor_y_position = Cursor.Position.Y;
                    try
                    {
                        int move_in_x = Int32.Parse(positions[0]);
                        int move_in_y = Int32.Parse(positions[1]);
                        
                        int changed_cursor_x_position = current_cursor_x_position + move_in_x;
                        int chagned_cursor_y_position = current_cursor_y_position + move_in_y;
                        // set mouse down on current cursor
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        // move cursor to the next position
                        Cursor.Position = new System.Drawing.Point(changed_cursor_x_position, chagned_cursor_y_position);
                        // release drag position
                        String wait = commands[2];
                        if (wait == "false")
                        {
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        }

                        message_to_show = "Success: Dragged mouse cursor";
                        response = "Request accepted.";
                    }
                    catch (Exception)
                    {
                        message_to_show = "Error: Invalid parameter passed.";
                        response = "Request denied.";
                    }
                }
                else if (commands[0] == "click")
                {
                    String which = commands[1];
                    if (which == "left")
                    {
                        simulate_mouse_left_click_single();
                        message_to_show = "Success: Mouse left click.";
                        response = "Request accepted.";
                    }
                    else if (which == "right")
                    {
                        simulate_mouse_right_click();
                        message_to_show = "Success: Mouse right click.";
                        response = "Request accepted.";
                    }
                    else
                    {
                        message_to_show = "Error: Invalid mouse click.";
                        response = "Request denied.";
                    }
                }
                else if (commands[0] == "double")
                {
                    simulate_mouse_left_click_single();
                    simulate_keyboard_enter_key();                    
                    message_to_show = "Success: Left button double click.";
                    response = "Request accepted.";
                }
                else
                {
                    response = "Invalid request.";
                    message_to_show = "Error: Invalid parameter passed.";
                }

                show_message(message_to_show);
                socket_send_resopnse(socket, response);
                socket_start_receiving_request(socket);
                server_start_accepting_connection();
            }

            
        }

        private void sendCallback(IAsyncResult asyncResult)
        {
            Socket socket = (Socket)asyncResult.AsyncState;
            socket.EndSend(asyncResult);
        }

        private void server_start_accepting_connection()
        {
            try
            {
                serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
            }
            catch
            {
                show_message("Error: Client was forcibly closed.");
            }
        }

        private Socket server_release_accepted_connection(IAsyncResult asyncResult)
        {
            try
            {
                return isServerStopped ? null : serverSocket.EndAccept(asyncResult);
            }
            catch (SocketException)
            {
                show_message("Error: Client was forcibly closed.");
                return null;
            }
        }

        private void socket_start_receiving_request(Socket socket)
        {
            try
            {
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
            }
            catch
            {
                show_message("Error: Client was forcibly closed.");
            }
        }

        private int socket_end_receiving_request(Socket socket, IAsyncResult asyncResult)
        {
            try
            {
                return socket.EndReceive(asyncResult);
            }
            catch (SocketException)
            {
                show_message("Error: Client was forcibly closed.");
                return 0;
            }
        }

        private void socket_send_resopnse(Socket socket, String response)
        {
            byte[] response_buffer_data = Encoding.ASCII.GetBytes(response);
            try
            {
                socket.BeginSend(response_buffer_data, 0, response_buffer_data.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
            }
            catch (SocketException)
            {
                show_message("Error: Client was forcibly closed.");
            }
        }

        private void show_message(string message)
        {
            this.form.add_log(message);
        }

        private void simulate_mouse_left_click_single()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void simulate_mouse_right_click()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private void simulate_keyboard_enter_key()
        {
            SendKeys.SendWait("{Enter}");
        }
        
    }
}
