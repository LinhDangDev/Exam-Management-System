using System.Text.RegularExpressions;

namespace GettingStarted.Client.Pages.Exam
{
    public partial class Exam
    {
        // xử lí các dạng kiểu dữ liệu
        private async Task modifyNhomCauHoi()
        {
            int stt = 0;
            if (customDeThis != null)
            {
                foreach (var item in customDeThis)
                {
                    if (item.MaNhom != stt && item.NoiDungCauHoiNhom != null)
                    {
                        stt = item.MaNhom;
                        item.NoiDungCauHoiNhom = await handleAudio(item.NoiDungCauHoiNhom);
                    }
                }
            }
        }
        private async Task<string?> handleAudio(string text)
        {
            //tìm thẻ tag audio
            if (!string.IsNullOrEmpty(text) && text.Contains("<audio"))
            {
                // lấy tên file name của audio
                int indexsource = text.IndexOf("source src=\"") + "source src=\"".Length; // phần đầu của source
                int indexendsource = text.IndexOf("\"/> </audio>"); // phần cuối của source
                string source = text.Substring(indexsource, indexendsource - indexsource);// source file ./audio/hello.mp3
                int index_filename = source.LastIndexOf("/");
                string filename = source.Substring(index_filename + 1);// tên filename
                int solannghe = 0;
                if (myData != null && myData.chiTietCaThi != null)
                    solannghe = await getSoLanNghe(myData.chiTietCaThi.MaChiTietCaThi, filename);
                // thêm 1 số function, lưu ý có 2 whitespace đầu và cuối
                string addText = $" controlsList=\"nodownload\" onplay=\"playMusic(this, 'listenedCount{maAudio}')\" ";
                int index = text.IndexOf("<audio"); // tìm chỉ số của "<audio"
                // thêm thông tin số lần nghe
                text = text.Insert(index + "<audio".Length, addText);
                // với thuộc tính id ta có thể thay đổi số lần nghe
                string addButton = $"<input class=\"fileAudio\" id=\"listenedCount{maAudio}\" type=\"button\" value=\"{solannghe}\" style=\"border-radius: 50%; border-style: none; font: 16px; cursor:not-allowed;\"></input>";
                text = text.Insert(text.Length, addButton);
                maAudio++;
            }
            return text ?? null;
        }
        private List<string> handleLatex(string text)
        {
            List<string> result = new List<string>();
            if (!text.Contains("latex"))
                return new List<string> { text };
            string[] parts = text.Split("<latex>");
            // xử lí phần đầu chắc chắn không có latex hoặc là thuần latex
            if(parts.Length > 1)
                result.Add(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                // phần cắt này chỉ có 2 phần duy nhất
                string[] parts2 = parts[i].Split("</latex>");

                // xử lí phần đầu chắc chắn là latex
                result.Add("$$" + parts2[0]);

                // phần còn lại là chữ hoặc không có nếu là thuần latex
                if(parts2.Length > 1)
                    result.Add(parts2[1]);
            }
            return result;
        }
        private string handleDienKhuyet(string text, int STT)
        {
            if(!text.Contains("(*)"))
                return text;
            return Regex.Replace(text, @"\(\*\)", m => "(" + (STT++).ToString() + ")");
        }
    }
}
