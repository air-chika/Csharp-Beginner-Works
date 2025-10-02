using SMPConsole;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 图片分拣机
{
    internal record BinNode(string aimFile, string srcFile, Image img);
    internal record Img(string file, Image img);

    public class ImgSpliter
    {

        readonly static int MaxCache = 499;

        readonly string infosFile = "memory\\info.txt";
        public string SourseFolder { get; private set; }
        public string AimFolder { get; set; }//完整路径
        LinkedList<Img> imgs;
        bool stopTask;
        Task task = new(() => { });
        public List<string> AimDirNames { get; private set; }//名称
        Stack<BinNode> binStack = new();
        bool DirRead;

        string aimNameFile;

        #region 回收

        public void WriteBin()
        {
            var x = binStack.Select(x => x.srcFile + " " + x.aimFile);
            File.WriteAllLines("memory\\bin.txt", x);
        }

        #endregion

        #region 动作

        public int RemainNum => imgs.Count;

        public Image? LastImg
        {
            get
            {
                if (imgs.Last == null)
                {
                    return null;
                }
                else
                    return imgs.Last.Value.img;
            }
        }

        public Image? Choose(string name)
        {
            if (imgs.Last == null)
            {
                return null;
            }
            string aim = Path.Combine(AimFolder, name);
            var srcfile = imgs.Last.Value.file;
            var aimfile = SmpFile.MoveNewFolder(srcfile, aim);
            binStack.Push(new(aimfile, srcfile, imgs.Last.Value.img));
            imgs.RemoveLast();
            return LastImg;
        }

        public Image? Revoke()
        {
            try
            {
                var bin = binStack.Pop();
                File.Move(bin.aimFile, bin.srcFile);
                imgs.AddLast(new Img(bin.srcFile, bin.img));
                return LastImg;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("撤回栈为空,撤回失败!");
                return LastImg;
            }
            catch
            {
                MessageBox.Show("文件路径出错,无法撤回!");
                return LastImg;
            }
        }


        #endregion

        #region 生成

        public ImgSpliter()
        {
            task.Start();
            task.Wait();
            var infos = new Queue<string>(File.ReadAllLines(infosFile));
            var x = infos.Dequeue();
            if (x == "f")
            {
                DirRead = false;
                LoadImg(GetFiles(infos.Dequeue()));
            }
            else
            {
                DirRead = true;
                LoadImg(GetDirFiles(infos.Dequeue()));
            }
            AimFolder = infos.Dequeue();
            ReadAimName(infos.Dequeue());
        }

        public IEnumerable<string> GetFiles(string sourseFolder)
        {
            SourseFolder = sourseFolder;
            DirRead = false;
            WriteInfo();
            return SearchPhotoFiles(sourseFolder);
        }

        public static IEnumerable<string> SearchPhotoFiles(string sourseFolder)
        {
            return Directory.GetFiles(sourseFolder).Where(x => x.EndsWith("gif", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("bmp", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("JPEG", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("PNG", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("JPG", StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<string> GetDirFiles(string sourseFolder)
        {
            SourseFolder = sourseFolder;
            DirRead = true;
            WriteInfo();
            return Directory.GetDirectories(sourseFolder).SelectMany(x => SearchPhotoFiles(x));
        }

        public void LoadImg(IEnumerable<string> imgFiles)
        {

            stopTask = true;
            if (!task.IsCompleted)
                task.Dispose();
            stopTask = false;
            imgs = new();

            Task.Run(() =>
            {
                int i = 0;
                var taskImg = imgs;
                Directory.CreateDirectory("cache");
                foreach (var file in imgFiles)
                {
                    if (i == MaxCache) return;
                    i++;
                    if (stopTask) return;
                    var cache = SmpFile.NewDirPath(file, "cache");
                    if (!File.Exists(cache) || !SmpForm.Photo.SamePhoto(file, cache))
                        File.Copy(file, cache, true);
                    try
                    {
                        taskImg.AddFirst(new Img(file, new Bitmap(cache)));
                    }
                    catch (Exception)
                    {
                        SmpFile.MoveNewFolder(file, "fail");
                    }
                }
            });
            Task.Delay(1000).Wait();
        }

        #endregion

        #region Aim

        public void AddAimName(string name)
        {
            AimDirNames.Add(name);
            WriteAimName();
        }

        public void RemoveAimName(string name)
        {
            AimDirNames.Remove(name);
            WriteAimName();
        }
        public void PreSetAimName(string name)
        {
            AimDirNames.Remove(name);
            AimDirNames.Insert(0, name);
            WriteAimName();
        }

        public void ReadAimName(string name)
        {
            aimNameFile = name;
            WriteInfo();
            AimDirNames = File.ReadAllLines(name).ToList();
        }

        void WriteAimName()
        {
            File.WriteAllLines(aimNameFile, AimDirNames);
        }

        public void WriteInfo()
        {
            File.WriteAllLines(infosFile, new string[]
            {
            (DirRead?"d":"f"),SourseFolder ,AimFolder,aimNameFile
            });
        }


        #endregion

    }


}
