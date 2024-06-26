public class Solution
{
    // shuffle the array order
    public int[] shuffle(int[] nums)
    {
        Random random = new Random()
        for (int i = 0; i < nums.Length; ++i)
        {
            int j = random.Next(i, nums.Length)
            nums[i] ^= nums[j]
            nums[j] ^= nums[i]
            nums[i] ^= nums[j]
        }
        return nums
    }
}