using System;

namespace Notepad_Library
{
    public class EditNotepad
    {
        public string DateTime_Now()
        {
            return DateTime.Now.ToString();
        }

        public FindNextResult FindNext(FindNextSearch search)
        {
            FindNextResult result = new FindNextResult();
            int position = -1;
            StringComparison s = search.MatchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            if (search.Direction == "UP")
            {
                position = search.Content.Substring(0, search.Position).LastIndexOf(search.SearchString, s);
                search.Success = position >= 0 ? true : false;
                result.SearchStatus = search.Success;
            }
            else
            {
                int start = search.Success ? search.Position + search.SearchString.Length : search.Position;
                position = start + search.Content.Substring(start, search.Content.Length - start).IndexOf(search.SearchString, s);
                search.Success = position - start >= 0 ? true : false;
                result.SearchStatus = search.Success;
            }
            result.SelectionStart = result.SearchStatus ? position : -1;
            return result;
        }
    }
}