namespace AreaDestination
{
   /// <summary>
   /// Set representing the possible alternatives to read a string an get the digits.
   /// </summary>
   public enum ParsingMode
   {
      /// <summary>
      /// Removes areas having a parent and returns single digits.
      /// </summary>
      EatChildReturnSingleDigits,
      /// <summary>
      /// Removes areas having a parent and returns compact representation.
      /// </summary>
      EatChildReturnRanges,
      /// <summary>
      /// Preserves areas having a parent and returns single digits.
      /// </summary>
      PreserveChildReturnSingleDigits,
      /// <summary>
      /// Preserves areas having a parent and returns  compact representation.
      /// </summary>
      PreserveChildReturnRanges
   }

   /// <summary>
   /// Set representing the possible compression that can be performed on areas.
   /// </summary>
   public enum Compression
   {
      /// <summary>
      /// Removes areas having parent in the same contest.
      /// </summary>
      Full,
      /// <summary>
      /// Preserves areas having parent in the same contest.
      /// </summary>
      Preserve
   }
}
