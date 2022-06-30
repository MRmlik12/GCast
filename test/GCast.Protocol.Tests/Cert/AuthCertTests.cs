using System.Text.RegularExpressions;
using GCast.Protocol.Cert;
using Xunit;

namespace GCast.Protocol.Test.Cert;

public class AuthCertTests
{
    private const string CertPattern =
        @"^-----BEGIN CERTIFICATE-----([\s\S]*)-----END CERTIFICATE-----\s?$";
    
    [Fact, Trait("type", "native-utils")]
    public void GetPeerCert_CheckIfContainsHeaders()
    {
        var cert = AuthCert.GetCert("192.168.1.22");
        var isMatches = cert != null && Regex.IsMatch(cert, CertPattern);
        
        Assert.True(isMatches);
    }
}