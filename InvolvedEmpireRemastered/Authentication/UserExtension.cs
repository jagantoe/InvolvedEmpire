namespace InvolvedEmpireRemastered.Authentication
{
    public static class UserExtensions
    {
        public static int GetTeamId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.First();
            return Convert.ToInt32(claim.Value);
        }
    }
}
