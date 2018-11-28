namespace STH.BiometricIdentityService.Api.Model
{
    public enum IdentityRequestType
    {
        MatchOnlyOne,
        MatchMoreThanOne,
        MatchNone,
        Verify,
        Delete,
        Enroll,
        ReEnroll
    }
}
