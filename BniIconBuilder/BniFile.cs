using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BniIconBuilder
{
    public class BniFile
    {

        public BniFormat Load(string fileName)
        {
            BniFormat bni;
            try
            {
                bni =_deserialize(fileName);
                if (bni.BniVersion != 0x1 || bni.HeaderLength != 0x10)
                    throw new FileLoadException();
            }
            catch
            {
                throw new Exception(string.Format("File {0} is not BNI archive.", fileName));
            }
            return bni;
        }

        public void Save(IconItem[] icons, byte[] data, string outFileName)
        {
            // sort items by flag
            sortIcons(ref icons);

            var bni = new BniFormat()
            {
                HeaderLength = 0x10,
                BniVersion = 0x01,
                NumIcons = icons.Length,
                Icons = icons,
                Data = data
            };

            // calc data start offset
            int offset = 16;
            foreach (IconItem i in icons)
            {
                offset += 16;
                if (i.Tag != null && i.Tag.Length == 4)
                    offset += 4;
            }
            bni.DataStart = offset;

            var bytes = _serialize(bni);

            File.WriteAllBytes(outFileName, bytes);
        }



        private byte[] _serialize(BniFormat bni)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(bni.HeaderLength);
                    bw.Write(bni.BniVersion);
                    bw.Write(bni.Unused);
                    bw.Write(bni.NumIcons);
                    bw.Write(bni.DataStart);

                    foreach (var i in bni.Icons)
                    {
                        bw.Write(i.Flag);
                        bw.Write(i.Width);
                        bw.Write(i.Height);
                        if (i.Tag != null && i.Tag.Length == 4)
                            // reverse tag and convert it to chars to exclude 0x04 byte at the beginning of normal string
                            bw.Write(reverse(i.Tag).ToCharArray()); 
                        bw.Write(i.Id);
                    }

                    bw.Write(bni.Data);
                }
                bytes = ms.GetBuffer();
            }
            return bytes;
        }

        private BniFormat _deserialize(string fileName)
        {
            var bni = new BniFormat();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    bni.HeaderLength = br.ReadInt32();
                    bni.BniVersion = br.ReadInt16();
                    bni.Unused = br.ReadInt16();
                    bni.NumIcons = br.ReadInt32();
                    bni.DataStart = br.ReadInt32();

                    bni.Icons = new IconItem[bni.NumIcons];

                    for (int i = 0; i < bni.NumIcons; i++)
                    {
                        bni.Icons[i].Flag = br.ReadInt32();
                        bni.Icons[i].Width = br.ReadInt32();
                        bni.Icons[i].Height = br.ReadInt32();

                        if (br.PeekChar() > 0)
                        {
                            bni.Icons[i].Tag = reverse(Encoding.ASCII.GetString(br.ReadBytes(4)));
                        }
                        bni.Icons[i].Id = br.ReadInt32();

                    }

                    bni.Data = br.ReadBytes((int)br.BaseStream.Length - bni.NumIcons);
                }
            }
            return bni;
        }

        private string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Sort items by flag; icons with tags always at the end of the array
        /// </summary>
        /// <param name="icons"></param>
        private void sortIcons(ref IconItem[] icons)
        {
            var iconList = icons.ToList();
            // select flags and order by flag
            var sortedIcons = icons.ToList().Where(x => string.IsNullOrEmpty(x.Tag)).OrderBy(x => x.Flag)
                // select tags and merge with flags (flags first)
                .Concat(iconList.Where(x => !string.IsNullOrEmpty(x.Tag)));

            icons = sortedIcons.ToArray();
        }

    }



    /// <summary>
    /// https://web.archive.org/web/20030603174754/http://kbg3.ath.cx/misc/bni_file.shtml
    /// </summary>
    public struct BniFormat
    {
        public Int32 HeaderLength;
        public Int16 BniVersion;
        public Int16 Unused;
        public Int32 NumIcons;
        public Int32 DataStart;

        public IconItem[] Icons;
        public byte[] Data;
    }

    public struct IconItem
    {
        public Bitmap Image;
        public Int32 Flag;
        public Int32 Width;
        public Int32 Height;
        public String Tag; // length = 4
        public Int32 Id;
    }

}
