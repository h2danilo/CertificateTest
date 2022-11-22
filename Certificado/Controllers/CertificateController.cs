using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Certificado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificateController : ControllerBase
    {
        [HttpGet]
        public ActionResult Certificate()
        {
            string pem = @"
-----BEGIN CERTIFICATE-----
MIIE8DCCAtigAwIBAgICBnowDQYJKoZIhvcNAQELBQAwgdIxCzAJBgNVBAYTAkJS
MRIwEAYDVQQIDAlTYW8gUGF1bG8xEjAQBgNVBAcMCVNhbyBQYXVsbzEgMB4GA1UE
CgwXUGFnU2VndXJvIEludGVybmV0IFMuQS4xIDAeBgNVBAsMF1BhZ1NlZ3VybyBJ
bnRlcm5ldCBTLkEuMSkwJwYDVQQDDCBzZWN1cmUuc2FuZGJveC5hcGkucGFnc2Vn
dXJvLmNvbTEsMCoGCSqGSIb3DQEJARYdbC1wYWdzZWd1cm8tYmVybGltQHVvbGlu
Yy5jb20wHhcNMjIxMTA5MjAxODAzWhcNMzIxMTA5MjAxODAzWjBgMQswCQYDVQQG
EwJCUjETMBEGA1UECAwKU8OjbyBQYXVsbzETMBEGA1UEBwwKU8OjbyBQYXVsbzEn
MCUGA1UEChMeRmVsaXBlIEFsZXNzYW5kZXIgQ2FldGFubyBMdWl6MIIBIjANBgkq
hkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnFuaNmNuYPTyoiy3OU6fQLknsVq5+s6b
oB1fjKghXuS917wLmgZX9OjBsCvC8G5F32ICLtTRg1m0NzfIfSklKq8LTOiyb2a6
LAzQ4y+ynl3/fGbr0iMCg7P2cWrpoagzXDpZcqAZLvgOPFhFrkEz0Hdgf7/nkdo2
CB77R4FMWW2Sena3pSiMdGqL7tfjowJNirNOhQkGfUJAJgnc78HreQXWCdapHgen
JQkAqRW4mVU0Bv9+cWbhDVNPtYasxdIKlfoUfzNagFcc6TY054Tlb1lHn7NgMcOO
4B/VK3064AxrPIkKrt1xw7sAMdTvxjumZNONKp1AF1HM1tyXfLTEcQIDAQABo0Ew
PzAOBgNVHQ8BAf8EBAMCB4AwHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMB
MA4GA1UdDgQHBAUBAgMEBjANBgkqhkiG9w0BAQsFAAOCAgEA24gDqXkiD5wDSRDS
ZJC57onXjqO+Db+q4imDxo5a5cgeAvS+lzzJv6+BearXRc6EeY3LaX3eoDoHAbki
GrEgeR7+ZvfD43pSeFdTOQaEBVlsdKhCrXVkVSshsEvQ9+jtehQ0rlE8f377v8kr
fI8z3sqDiVjivx6Z0Hb76vvBABDz/hzejLOiVnYTCi6z54sNAjTlqZHIPrin8BWB
fb88Hdz1PAZ+xmMFoTog/z5fwhPhbA7WJe+uNl5WYAndz8+5yul4A1Yi0Y9VW04e
mADNzKE+K+8vApn7Ah63rDSOghHNiFrNBvaVZ8g3h8mpIDRrtvJG4gLjmnE929uS
HaY1VJhYsRcDMdJwx63bRDiYL4V3rhAWGsyGryMYaX1GnKv9p9GhoZnxJVr+wOsy
L+nrVKEllujsT975tK+JxLti7fC99N4Rr6bXZ0O3p1wZ46pzV1YD2GlTgJtpY00i
3H2JXNG8m3tlCnLMI57s+Er+Z4aBCTpglGV2/kJDabJdYPwRhUz3oyAZsv2pgCeG
Q8B5OAnmLVfA8Kw80jMW24oCsAnqU3FKHqxuSI64zwlAWtUjY84wh5yvjCBryHXn
OLR18is/iKut2Jexb7XOmnSl/rUssqYwAmkTGepFFPx5ZSWALMfhz1nlFEyCqL7S
Sw8xcFGADL4HoHl88465yLXNKvg=
-----END CERTIFICATE-----
";

            string prikey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAnFuaNmNuYPTyoiy3OU6fQLknsVq5+s6boB1fjKghXuS917wL
mgZX9OjBsCvC8G5F32ICLtTRg1m0NzfIfSklKq8LTOiyb2a6LAzQ4y+ynl3/fGbr
0iMCg7P2cWrpoagzXDpZcqAZLvgOPFhFrkEz0Hdgf7/nkdo2CB77R4FMWW2Sena3
pSiMdGqL7tfjowJNirNOhQkGfUJAJgnc78HreQXWCdapHgenJQkAqRW4mVU0Bv9+
cWbhDVNPtYasxdIKlfoUfzNagFcc6TY054Tlb1lHn7NgMcOO4B/VK3064AxrPIkK
rt1xw7sAMdTvxjumZNONKp1AF1HM1tyXfLTEcQIDAQABAoIBAQCCWauapBIlKpAM
u5KUaANEBhgdNzHM8emz9kjuDkyju7a438AcTCWPu2ud3x3JK2V88Uw4lpuEEYi4
b/zouyCNERkuqN/5F38lshmDDQSwGlUf9P2sVhuGiMVvkF3tszf+L3bapLXZ/lp/
3Gda+mij5qHAXtBDO9PHuWxrFFP+SJk1yjHmXVAuBUsfpOZh4yE0QCwbWPA6gvwJ
BM9lcJR0FMRc24XxuPg+mSNcaOCZHcpH+GoOogSFCxfGkHSXUSO4HfVKJnfj1/tX
xGT5v2S+oGwpxJZ3lFKRUIoO8GG9a0n1pQP/fCoB4HIg1AsgU5dc64vWchmYaXBD
fwrFx2TxAoGBAMpEjK+6Xd4k3P87qFFoG+PC7P/VEs/baBTtOXlSz4r48b4/Nwzl
8RxM3LuNCw7gb/6V6p4EPLxbhVnJeHx8oWfTyugYhlQYXHT22hi2ZX37+gobCJin
Iqm39ieXd9Bfn89O/Fe9i5GCK/kwduhOEC++VFcNnQbGap/seyB1hqxTAoGBAMXk
5PQHbvtmj7yxwuTx94xADkacmJ3ivdGT70sIDwZidcZ66dD4/PM0ANqSTc/HQmqp
UHepcHMKm2PplJMFX11ZpqrRHTTWq9lL0VIMB7PHPO3Q6C2JMOZ625A69yZnGUPt
x4EfnMNO/EHLMS3jpMiIYh6x5hQLcuoOwzuEhJOrAoGAO9shTvd1w8Fg7N74pm6K
olIS1Hp3gK9LE7ZH5dFNvbCByWznm5eITHmqcI4Ckbp1Kf+vWGekmZDz22g1kZFI
KnOQfjKTvdsfsTVDwUBotGmWytrJJnN/XH4mB/1Y5X8GydBz+HsNDFhbLvjal1BS
le5bAw/yQgkBkKXooehlnCUCgYBnAojzRdNNZcu4S0fJ6PIUvVsxAl6nANoR99F9
2woHJsVrtZt0PcddyVoQbEu4tl4qK0xJnZd9Kq5MSdAe35MOgpYxIHz1fjEiAUdK
UBzKRhG/MRlKarlbn+QepbL0+IG8TE+nNQ9i0wCYNu64lmC2SdfZZnz/Ph5pG5og
pxBj1QKBgBUHWMCjn3x7hpikCGOcXVKKc2+ETDMduiMdHnvj6fvHzZ9vcraDYK9n
h4s9btSzmYLky550h7QgB3u19xqXF5e8hsEWSanQ2nDr9zb9DL8XoCa4sOXZWMNz
vwkcac2/UhvRTSBtaSjW502ouKoIuZcRsKXozFdvKl3bo3CfOqCp
-----END RSA PRIVATE KEY-----";


            try
            {
                var certnew = GetCertificateFromPEMstring(pem, prikey, "");
                return Ok(certnew);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        public static X509Certificate2 GetCertificateFromPEMstring(string publicCert, string privateKey, string password)
        {
            byte[] certBuffer = Helpers.GetBytesFromPEM(publicCert, PemStringType.Certificate);
            byte[] keyBuffer = Helpers.GetBytesFromPEM(privateKey, PemStringType.RsaPrivateKey);

            X509Certificate2 certificate = new X509Certificate2(certBuffer, password);


            RSACryptoServiceProvider prov = Crypto.DecodeRsaPrivateKey(keyBuffer);
            certificate.PrivateKey = prov;

            return certificate;
        }
    }
}
