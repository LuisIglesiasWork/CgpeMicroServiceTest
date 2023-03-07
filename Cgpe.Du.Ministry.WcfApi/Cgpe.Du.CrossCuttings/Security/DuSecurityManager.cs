using Cgpe.Du.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Cgpe.Du.CrossCuttings
{

    public class DuSecurityManager
    {

        private Dictionary<Guid, DuPolicy> policies;

        public DirectoryUser LoggedUser { get; set; }

        public DuSecurityManager()
        {
            this.LoggedUser = Thread.CurrentPrincipal as DirectoryUser;
            if (this.LoggedUser == null)
                throw new Exception("Logged user is not valid.");

            this.policies = new Dictionary<Guid, DuPolicy>();
            this.policies.Add(DuPolicies.CreateProcurator, new DuPolicy()
            {
                PolicyId = DuPolicies.CreateProcurator,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });
            this.policies.Add(DuPolicies.ReadProcurator, new DuPolicy()
            {
                PolicyId = DuPolicies.ReadProcurator,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });
            this.policies.Add(DuPolicies.UpdateProcurator, new DuPolicy()
            {
                PolicyId = DuPolicies.UpdateProcurator,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });
            this.policies.Add(DuPolicies.CreateAssociation, new DuPolicy()
            {
                PolicyId = DuPolicies.CreateAssociation,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });
            this.policies.Add(DuPolicies.ReadAssociation, new DuPolicy()
            {
                PolicyId = DuPolicies.ReadAssociation,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });
            this.policies.Add(DuPolicies.UpdateAssociation, new DuPolicy()
            {
                PolicyId = DuPolicies.UpdateAssociation,
                Check =
                procurator =>
                {
                    return LoggedUser.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "X5734915A");
                }
            });




        }

        public void CheckPermission(params Guid[] claimIds)
        {
            if (!(this.LoggedUser != null && this.LoggedUser.DirectoryRoles.Any(r => r.Claims.Any(c =>claimIds.Contains(c.ClaimId)))))
            {
                throw new UnauthorizedAccessException(this.GetPermissionExceptionMessage(claimIds));
            };
        }

        public void CheckPermission(Guid generalClaimId, Guid particularClaimId, Guid? associationId)
        {
            bool authorized = false;

            if (this.LoggedUser != null)
            {
                if (this.LoggedUser.DirectoryRoles.Any(r => r.Claims.Any(c => c.ClaimId == generalClaimId)))
                {
                    authorized = true;
                }
                else if (this.LoggedUser.Association != null && associationId.HasValue)
                {
                    if (this.LoggedUser.DirectoryRoles.Any(r => r.Claims.Any(c => c.ClaimId == particularClaimId))
                            && this.LoggedUser.Association.AssociationId == associationId.Value)
                    {
                        authorized = true;
                    }
                }
            }

            if (!authorized)
            {
                GetPermissionExceptionMessage(generalClaimId);
            }
        }

        public void CheckPermission(Guid particularClaimId, Guid? associationId)
        {
            bool authorized = false;

            if (this.LoggedUser != null)
            {
                if (this.LoggedUser.Association != null && associationId.HasValue)
                {
                    if (this.LoggedUser.DirectoryRoles.Any(r => r.Claims.Any(c => c.ClaimId == particularClaimId))
                            && this.LoggedUser.Association.AssociationId == associationId.Value)
                    {
                        authorized = true;
                    }
                }
            }

            if (!authorized)
            {
                GetPermissionExceptionMessage(particularClaimId);
            }
        }

        

        public void CheckPermission(Guid claimId)
        {
            if (!(this.LoggedUser != null && this.LoggedUser.DirectoryRoles.Any(r => r.Claims.Any(c => c.ClaimId == claimId))))
            {
                throw new UnauthorizedAccessException(this.GetPermissionExceptionMessage(claimId));
            };
        }

        private string GetPermissionExceptionMessage(params Guid[] claimIds)
        {
            if (claimIds.Contains(DuClaimTypes.BOE))
            {
                return Properties.Resources.UnathorizedAccessBOE;
            }
            else if (claimIds.Contains(DuClaimTypes.Notary))
            {
                return Properties.Resources.UnathorizedAccessNotary;
            }
            else if (claimIds.Contains(DuClaimTypes.Cgpj))
            {
                return Properties.Resources.UnathorizedAccessCGPJ;
            }
            else if (claimIds.Contains(DuClaimTypes.CgpeInternal))
            {
                return Properties.Resources.UnathorizedAccessCgpeInternal;
            }
            else if (claimIds.Contains(DuClaimTypes.Registrator))
            {
                return Properties.Resources.UnauthorizedAccessRegistrator;
            }
            else if (claimIds.Contains(DuClaimTypes.CreateUser) || claimIds.Contains(DuClaimTypes.CreateAnyUser))
            {
                return Properties.Resources.UnauthorizedAccessCreateUser;
            }
            else if (claimIds.Contains(DuClaimTypes.UpdateUser) || claimIds.Contains(DuClaimTypes.UpdateAnyUser))
            {
                return Properties.Resources.UnauthorizedAccessUpdateUser;
            } else if (claimIds.Contains(DuClaimTypes.QueryUser) || claimIds.Contains(DuClaimTypes.QueryAnyUser))
            {
                return Properties.Resources.UnauthorizedAccessQueryUsers;
            }
            else
            {
                return Properties.Resources.UnauthorizedGeneric;
            }
        }

        public bool CheckSecurity(Guid policyId, Procurator procurator)
        {
            if (this.policies.ContainsKey(policyId))
                return this.policies[policyId].Check(procurator);
            return false;
        }

        public void SignatureValidate(string signedData, object data)
        {
            if (string.IsNullOrWhiteSpace(signedData))
                throw new ArgumentException("JSON has no signature.");
            if (data == null)
                throw new ArgumentException("There is no data to validate.");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy { }
            };
            JObject json = JObject.FromObject(data, new JsonSerializer
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
            });
            this.ClearJson(json);
            DirectoryUser loggedUser = Thread.CurrentPrincipal as DirectoryUser;
            if (loggedUser == null)
                throw new Exception("Logged user is not valid.");
            X509Certificate2 clientCertificate = loggedUser.ClientCertificate;
            var signature = Convert.FromBase64String(signedData);

            var hashProvider = new SHA1Managed();
            var jsonString = json.ToString();
            jsonString = jsonString.Replace("+00:00", "Z");
            var content = Encoding.UTF8.GetBytes(jsonString);
            byte[] hash = hashProvider.ComputeHash(content);

            var rsaProvider = (RSA)clientCertificate.PublicKey.Key;
            if (!rsaProvider.VerifyHash(hash, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1))
            {
                throw new Exception("Signature is invalid. " + json.ToString() + " " + clientCertificate.Subject);
            }
        }

        private void ClearNode(JToken node)
        {
            JToken current = node;
            JValue jvalue = null;
            JProperty property = null;
            if (current.Type == JTokenType.Property)
            {
                property = current as JProperty;
                current = property.Value;
                jvalue = property.Value as JValue;
                if (jvalue.Type == JTokenType.Guid && jvalue.Value<Guid>() == Guid.Empty)
                {
                    this.RemoveNode(property);
                    return;
                }
            }

            if (current.Type == JTokenType.String)
            {
                string value = current.Value<string>();
                if (string.IsNullOrWhiteSpace(value) || value.Trim() == "00000000-0000-0000-0000-000000000000")
                {
                    if (property != null)
                        this.RemoveNode(property);
                    else
                        this.RemoveNode(current);
                }
            }



            if (current.Type == JTokenType.Guid)
            {
                if (current.Value<Guid>() == Guid.Empty)
                {
                    if (property != null)
                        this.RemoveNode(property);
                    else
                        this.RemoveNode(current);
                }
            }
            if (current.Type == JTokenType.Array || current.Type == JTokenType.Object)
            {
                throw new NotSupportedException("Node Type is not supported.");
            }
            if (current.Type == JTokenType.None)
            {
                throw new Exception("Token Type is empty.");
            }
        }

        private void RemoveNode(JToken node)
        {
            if (node.Parent != null && node.Parent.Type == JTokenType.Property && node.Parent.Parent != null)
                node.Parent.Remove();
            else if (node.Parent != null)
                node.Remove();
        }

        private void ClearJson(JToken node)
        {
            List<JToken> children = node.Children().ToList();
            JToken current;
            JProperty property;
            for (int i = 0; i < children.Count; i++)
            {
                current = children[i];
                property = null;
                if (current.Type == JTokenType.Property)
                {
                    property = current as JProperty;
                    current = property.Value;
                }
                if (current.Type == JTokenType.Array || current.Type == JTokenType.Object)
                {
                    this.ClearJson(current);
                    if (current.Children().Count() == 0)
                    {
                        if (property != null)
                            this.RemoveNode(property);
                        else
                            this.RemoveNode(current);
                    }
                }
                else
                {
                    if (property != null)
                        this.ClearNode(property);
                    else
                        this.ClearNode(current);
                }
            }
        }

    }

}
