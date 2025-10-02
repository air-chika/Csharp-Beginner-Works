using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace SMPConsole
{
    public static class SmpFile
    {
        public static void CreateShortcut(string directory, string shortcutName, string targetPath,
        string? description = null, string? iconLocation = null)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            string shortcutPath = Path.Combine(directory, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            //shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }

        public static bool IsFileName(string name)
        {
            if (name.All(x => x == ' ')) return false;
            if (name.Any(x => Path.GetInvalidFileNameChars().Contains(x))) return false;
            return true;
        }

        public static bool IsDirectoryName(string name)
        {
            if (name.All(x => x == ' ')) return false;
            if (name.Any(x => Path.GetInvalidPathChars().Contains(x))) return false;
            return true;
        }


        public static string FileNameSet(string source, Func<string, string> ope) =>
            Path.Combine(Path.GetDirectoryName(source) ?? "", ope(Path.GetFileNameWithoutExtension(source)) + Path.GetExtension(source));

        public static string NewDirPath(string file, string dir, bool overwrite = true)
        {
            var newPath = Path.Combine(dir, Path.GetFileName(file));
            if (overwrite) return newPath;
            while (System.IO.File.Exists(newPath))
                newPath = FileNameSet(newPath, x => x + '.');
            return newPath;
        }

        //移动至新路径文件夹,非重写会加.  返回新的文件路径
        public static string MoveNewFolder(string source, string desFolder, bool overwrite = false)
        {
            Directory.CreateDirectory(desFolder);
            string newPath = NewDirPath(source, desFolder, overwrite);
            System.IO.File.Move(source, newPath, true);
            return newPath;
        }
        //复制至新路径文件夹,非重写会加.  返回新的文件路径
        public static string CopyNewFolder(string source, string desFolder, bool overwrite = false)
        {
            Directory.CreateDirectory(desFolder);
            string newPath = NewDirPath(source, desFolder, overwrite);
            System.IO.File.Copy(source, newPath, true);
            return newPath;
        }


        public static IEnumerable<string> SearchPhotoFiles(string sourseFolder)
        {
            return Directory.GetFiles(sourseFolder).Where(x => x.EndsWith("gif", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("bmp", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("JPEG", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("PNG", StringComparison.CurrentCultureIgnoreCase) || x.EndsWith("JPG", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
