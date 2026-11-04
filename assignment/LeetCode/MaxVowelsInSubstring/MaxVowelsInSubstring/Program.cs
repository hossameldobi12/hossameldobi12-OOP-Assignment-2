
string s = "leetcode";
int k = 3;
Console.WriteLine(new Solution().MaxVowels(s, k));




public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int count = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                count++;
        }

        int maxVowels = count;
        int left = 0;

        for (int right = k; right < s.Length; right++)
        {
            if (IsVowel(s[left]))
                count--;

            if (IsVowel(s[right]))
                count++;

            left++;

            maxVowels = Math.Max(maxVowels, count);
        }

        return maxVowels;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i' ||
               c == 'o' || c == 'u';
    }
}
