using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Paint
{
    internal static class VersionControl
    {
        static readonly string vc_path = @".\VersionControl";
        static readonly string vc_mainFile = "BRANCHES.csv";
        static readonly int commit_name_length = 6;
        static readonly int branch_name_length = 4;
        private static FileStream main_fs;
        readonly private static Random random = new Random();
        static string branch_name, full_path, current, last;

        public static List<Element> elem = new List<Element>();

        private static FileStream main_file()
        {
            if (!File.Exists(add_vc_path(vc_mainFile)))
            {
                main_fs = create_main();
            }
            else
            {
                main_fs = open_main();
            }
            return main_fs;
        }

        private static FileStream commit_file(string path)
        {
            FileStream fs;
            if (!File.Exists(add_vc_path(path)))
            {
                fs = create_commit(path);
            }
            else
            {
                fs = open_commit(path);
            }
            return fs;
        }

        private static void write_from_start(FileStream fs, string text)
        {
            fs.SetLength(seek_to_second_line(fs));
            byte[] str_bytes = new UTF8Encoding(true).GetBytes(text);
            fs.Write(str_bytes, 0, str_bytes.Length);
        }

        private static FileStream open_main()
        {
            main_fs = File.Open(add_vc_path(vc_mainFile), FileMode.Open);
            return main_fs;
        }

        private static FileStream open_commit(string path)
        {
            FileStream fs = File.Open(add_vc_path(path), FileMode.Open);
            return fs;
        }

        private static FileStream create_commit(string path)
        {
            FileStream fs = File.Open(add_vc_path(path), FileMode.Create);
            fs.SetLength(0);
            write_str(fs, "#,x1,y1,x2,y2,shape,size,color,-,x1,y1,x2,y2,shape,size,color\n");
            fs.Close();
            fs = open_commit(path);
            return fs;
        }

        private static FileStream create_main()
        {
            main_fs = File.Open(add_vc_path(vc_mainFile), FileMode.Create);
            main_fs.SetLength(0);
            write_str(main_fs, "branch_name,full_path,current,last\n");
            main_fs.Close();
            main_fs = open_main();
            return main_fs;
        }

        private static long seek_to_symbol(FileStream fs, long seek, SeekOrigin origin, char symbol)
        {
            fs.Seek(seek, origin);
            long counter = 0;
            while (true)
            {
                int b = fs.ReadByte();
                counter++;
                if (b == -1) break;
                char c = Convert.ToChar(b);
                if (c == symbol) break;
            }
            return counter;
        }

        private static long seek_to_second_line(FileStream fs)
        {
            return seek_to_symbol(fs, 0, SeekOrigin.Begin, '\n');
        }

        private static string add_vc_path(string path)
        {
            return $@"{vc_path}\{path}";
        }

        private static void write_str(FileStream fs, string str)
        {
            byte[] str_bytes = new UTF8Encoding(true).GetBytes(str);
            fs.Write(str_bytes, 0, str_bytes.Length);
        }

        private static void main_dir()
        {
            if (!Directory.Exists(vc_path))
            {
                DirectoryInfo vc_dir = Directory.CreateDirectory(vc_path);
            }
        }

        public static void commit()
        {
            main_dir();
            string main_line = main_info();

            string path = full_path.Split(new string[] { current }, StringSplitOptions.None)[0] + current;
            string new_commit_name = RandomString(commit_name_length);

            string new_folder_path = string.IsNullOrWhiteSpace(path) ? new_commit_name : $@"{path}\{new_commit_name}";
            DirectoryInfo new_folder = Directory.CreateDirectory(add_vc_path(new_folder_path));

            string new_file_path = new_folder_path + $@"\{new_commit_name}.csv";

            string diff = calculate_diff();
            using (FileStream fs = commit_file(new_file_path))
            {
                write_from_start(fs, diff);
            }

            long seek;
            if (string.IsNullOrWhiteSpace(branch_name))
            {
                using (FileStream main_fs = open_main())
                {
                    seek = seek_to_second_line(main_fs);
                }
                string new_branch_name = RandomString(branch_name_length);
                add_branch_to_main(new_branch_name, new_folder_path, new_commit_name, new_commit_name);
            }
            else
            {
                using (FileStream main_fs = open_main())
                {
                    seek = seek_of(main_fs, main_line);
                }

                if (current == last)
                {
                    update_branch_in_main(branch_name, new_folder_path, new_commit_name, new_commit_name, seek);
                }
                else
                {
                    update_branch_in_main(branch_name, full_path, "", last, seek);
                    string new_branch_name = RandomString(branch_name_length);
                    add_branch_to_main(new_branch_name, new_folder_path, new_commit_name, new_commit_name);
                }
            }

        }

        private static bool elems_equal(Element el1, Element el2)
        {
            try
            {
                if (el1.coords.point1.X != el2.coords.point1.X) return false;
                if (el1.coords.point1.Y != el2.coords.point1.Y) return false;
                if (el1.coords.point2.X != el2.coords.point2.X) return false;
                if (el1.coords.point1.Y != el2.coords.point1.Y) return false;
                if ((int)el1.shape != (int)el2.shape) return false;
                if (el1.size != el2.size) return false;
                if (el1.color != el2.color) return false;
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private static string calculate_diff()
        {
            string result = "";
            // #,x1,y1,x2,y2,shape,size,color,-,x1,y1,x2,y2,shape,size,color
            if (string.IsNullOrWhiteSpace(branch_name))
            {
                // initial diff
                for(int i = 0; i < elem.Count; i++)
                {
                    Element el = elem[i];
                    result += $"{i},,,,,,,,-,{el.coords.point1.X},{el.coords.point1.Y},{el.coords.point2.X},{el.coords.point2.Y},{(int)(el.shape)},{el.size},{el.color}\n";
                }
            }
            else
            {
                // diff
                List<Element> current_commited = load_current();
                for (int i = 0; i < elem.Count && i < current_commited.Count; i++)
                {
                    if (!elems_equal(elem[i], current_commited[i]))
                    {
                        result += $"{i},{current_commited[i].coords.point1.X},{current_commited[i].coords.point1.Y},{current_commited[i].coords.point2.X},{current_commited[i].coords.point2.Y},{current_commited[i].shape},{current_commited[i].size},{current_commited[i].color},-,{elem[i].coords.point1.X},{elem[i].coords.point1.Y},{elem[i].coords.point2.X},{elem[i].coords.point2.Y},{(int)(elem[i].shape)},{elem[i].size},{elem[i].color}\n";
                    }
                }
                if (current_commited.Count > elem.Count)
                {
                    for (int i = elem.Count; i < current_commited.Count; i++)
                    {
                        result += $"{i},{current_commited[i].coords.point1.X},{current_commited[i].coords.point1.Y},{current_commited[i].coords.point2.X},{current_commited[i].coords.point2.Y},{current_commited[i].shape},{current_commited[i].size},{current_commited[i].color},-,,,,,,,\n";
                    }
                }
                else if (current_commited.Count < elem.Count)
                {
                    for (int i = current_commited.Count; i < elem.Count; i++)
                    {
                        result += $"{i},,,,,,,,-,{elem[i].coords.point1.X},{elem[i].coords.point1.Y},{elem[i].coords.point2.X},{elem[i].coords.point2.Y},{(int)(elem[i].shape)},{elem[i].size},{elem[i].color}\n";
                    }
                }
            }

            return result;
        }

        private static List<Element> load_current()
        {
            List<Element> commit = new List<Element>();
            string main_line = main_info();

            // all commit .csv paths
            List<string> paths = new List<string>();
            string[] commits = (full_path.Split(new string[] { current }, StringSplitOptions.None)[0] + current).Split('\\');
            for (int i = 0; i < commits.Length; i++)
            {
                string path = String.Join("\\", SubArray<string>(commits, 0, i + 1));
                paths.Add($"{path}\\{commits[i]}.csv");
            }

            for (int i = 0; i < paths.Count; i++)
            {
                long seek;
                using (FileStream fs = open_commit(paths[i]))
                {
                    seek = seek_to_second_line(fs);
                    while(true)
                    {
                        string diff_line = diff_line_info(fs, out int id, out Element el_before, out Element el_after, seek);
                        if (id == -1)
                        {
                            break;
                        }
                        else if (el_before.coords.point1.X == -1)
                        {
                            commit.Add(el_after);
                        }
                        else if (el_after.coords.point1.X == -1)
                        {
                            commit.Remove(el_before);
                        }
                        else
                        {
                            commit[id] = el_after;
                            if (id != commit.Count - 1)
                            {
                                throw new Exception("id != commit.Count-1 (load_current)");
                            }
                        }
                    }
                }
            }


            return commit;
        }

        private static string diff_line_info(FileStream fs, out int id, out Element el_before, out Element el_after, long seek)
        {
            // #,x1,y1,x2,y2,shape,size,color,-,x1,y1,x2,y2,shape,size,color
            string result = "";
            id = -1;
            el_before = new Element();
            el_after = new Element();
            while(true)
            {
                int b = fs.ReadByte();
                if (b == -1) return result;
                char c = Convert.ToChar(b);
                result += c;
                if (c == '\n') break;
            }
            string[] columns = result.Split(',');

            id = Int32.Parse(columns[0]);
            if (!string.IsNullOrWhiteSpace(columns[1]))
            {
                el_before.coords.point1.X = int.Parse(columns[1]);
                el_before.coords.point1.Y = int.Parse(columns[2]);
                el_before.coords.point2.X = int.Parse(columns[3]);
                el_before.coords.point2.Y = int.Parse(columns[4]);
                el_before.shape = (Shape)int.Parse(columns[5]);
                el_before.size = int.Parse(columns[6]);
                el_before.color = int.Parse(columns[7]);
            }
            if (!string.IsNullOrWhiteSpace(columns[9]))
            {
                el_after.coords.point1.X = int.Parse(columns[9]);
                el_after.coords.point1.Y = int.Parse(columns[10]);
                el_after.coords.point2.X = int.Parse(columns[11]);
                el_after.coords.point2.Y = int.Parse(columns[12]);
                el_after.shape = (Shape)int.Parse(columns[13]);
                el_after.size = int.Parse(columns[14]);
                el_after.color = int.Parse(columns[15]);
            }
            return result;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        private static string read_file(FileStream fs)
        {
            byte[] bytes = new byte[fs.Length];
            int numBytesToRead = (int)fs.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;

            string bytes_str = Encoding.Default.GetString(bytes);
            return bytes_str;
        }

        private static long seek_of(FileStream fs, string str)
        {
            string bytes_str = read_file(main_fs);
            long seek = bytes_str.IndexOf(str);
            return seek;
        }

        private static string main_info()
        {
            branch_name = full_path = current = last = "";
            string result = "";

            string bytes_str;
            using (FileStream main_fs = main_file())
            {
                bytes_str = read_file(main_fs);
            }
            string[] lines = bytes_str.Split('\n');

            if (lines.Length < 2 || string.IsNullOrWhiteSpace(lines[1])) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(',');
                branch_name = columns[0];
                full_path = columns[1];
                current = columns[2];
                last = columns[3];

                if (!string.IsNullOrWhiteSpace(current))
                {
                    result = line + "\n";
                    break;
                }
            }
            return result;
        }

        private static long seek_to_next_line(FileStream fs, long seek)
        {
            string whole_file = read_file(fs);
            long result = whole_file.IndexOf('\n', (int)seek);
            return result;
        }

        private static void update_branch_in_main(string branch_name, string full_path, string current, string last, long seek)
        {
            string str = $"{branch_name},{full_path},{current},{last}\n";
            long to = seek + str.Length;
            long from;
            using (FileStream main_fs = open_main())
            {
                from = seek_to_next_line(main_fs, seek) + 1;
                shift(main_fs, from, to);
            }

            using (FileStream main_fs = open_main())
            {
                main_fs.Seek(seek, SeekOrigin.Begin);
                write_str(main_fs, str);
            }
        }

        private static void shift(FileStream fs, long from, long to)
        {
            if (from > to)
            {
                long seek_end = fs.Seek(0, SeekOrigin.End);
                while (from < seek_end)
                {
                    fs.Seek(from, SeekOrigin.Begin);
                    int b = fs.ReadByte();

                    char c = Convert.ToChar(b);

                    fs.Seek(to, SeekOrigin.Begin);
                    fs.WriteByte((byte)b);
                    from++;
                    to++;
                }
                fs.SetLength(to);
            }
            else
            {
                long cur_from = fs.Seek(0, SeekOrigin.End) - 1; // fs.Seek(1, SeekOrigin.End) ?
                long cur_to = cur_from + (to - from);
                while (cur_from >= from)
                {
                    fs.Seek(cur_from, SeekOrigin.Begin);
                    int b = fs.ReadByte();
                    
                    char c = Convert.ToChar(b);

                    fs.Seek(cur_to, SeekOrigin.Begin);
                    fs.WriteByte((byte)b);
                    cur_from--;
                    cur_to--;
                }
            }
        }

        private static void add_branch_to_main(string branch_name, string full_path, string current, string last)
        {
            using (FileStream main_fs = open_main())
            {
                string str = $"{branch_name},{full_path},{current},{last}\n";
                main_fs.Seek(0, SeekOrigin.End);
                write_str(main_fs, str);
            }
        }

        public static void next_branch()
        {
            string branch_line = main_info();
            long seek_old;
            using (FileStream main_fs = open_main())
            {
                seek_old = seek_of(main_fs, branch_line);
            }
            long seek_new;
            long seek_end;
            using (FileStream main_fs = open_main())
            {
                seek_end = main_fs.Seek(0, SeekOrigin.End);
            }

            if (seek_old + branch_line.Length >= seek_end)
            {
                using (FileStream main_fs = open_main())
                {
                    seek_new = seek_to_second_line(main_fs);
                }
            }
            else
            {
                seek_new = seek_old + branch_line.Length - commit_name_length;
            }
            update_branch_in_main(branch_name, full_path, "", last, seek_old);
            branch_info(out string branch_name1, out string full_path1, out string current1, out string last1, seek_new);
            update_branch_in_main(branch_name1, full_path1, last1, last1, seek_new);
            branch_line = main_info();
            elem = load_current();
        }

        private static string branch_info(out string branch_name, out string full_path, out string current, out string last, long seek)
        {
            string bytes_str;
            using (FileStream main_fs = open_main())
            {
                bytes_str = read_file(main_fs);
            }
            string result = bytes_str.Substring((int)seek, bytes_str.IndexOf('\n', (int)seek) - (int)seek);
            string[] columns = result.Split(',');
            branch_name = columns[0];
            full_path = columns[1];
            current = columns[2];
            last = columns[3];
            return result;
        }

        public static void next_commit()
        {
            string main_line = main_info();
            int pos_of_current = full_path.IndexOf(current);

            if (!full_path.Substring(pos_of_current).Contains(@"\")) return;

            int pos_of_next = full_path.IndexOf(@"\", pos_of_current) + 1;
            int end_of_next;
            if (full_path.Substring(pos_of_next).Contains(@"\"))
            {
                end_of_next = full_path.IndexOf(@"\", pos_of_next) - 1;
            }
            else
            {
                end_of_next = full_path.Length - 1;
            }
            string new_current = full_path.Substring(pos_of_next, end_of_next - pos_of_next + 1);

            long seek;
            using (FileStream main_fs = open_main())
            {
                seek = seek_of(main_fs, main_line);
            }
            update_branch_in_main(branch_name, full_path, new_current, last, seek);
            main_line = main_info();
            elem = load_current();
        }

        public static void prev_commit()
        {
            string main_line = main_info();
            int end_of_prev = full_path.IndexOf(current) - 2;

            if (end_of_prev < 0) return;

            int pos_of_prev = full_path.LastIndexOf(@"\", end_of_prev) + 1;
            if (pos_of_prev < 0) pos_of_prev = 0;
            string new_current = full_path.Substring(pos_of_prev, end_of_prev - pos_of_prev + 1);

            long seek;
            using (FileStream main_fs = open_main())
            {

                seek = seek_of(main_fs, main_line);
            }
            update_branch_in_main(branch_name, full_path, new_current, last, seek);
            main_line = main_info();
            elem = load_current();
        }
    }
}
