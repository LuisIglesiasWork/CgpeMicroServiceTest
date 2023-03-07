#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
FROM mcr.microsoft.com/dotnet/framework/wcf:4.8-windowsservercore-ltsc2019
ARG source

RUN mkdir C:\Du3Webservice

# configure the new site in IIS.
RUN powershell -NoProfile -Command \
Import-module IISAdministration; \
New-IISSite -Name "Du3WebService" -PhysicalPath C:\Du3Webservice -BindingInformation "*:44318:"

RUN Import-Module WebAdministration; Set-ItemProperty "IIS:\\Sites\\Du3Webservice" -name bindings -value (@{protocol='net.tcp';bindingInformation='44318:*'},@{protocol='http';bindingInformation='*:44318:'});
RUN windows\system32\inetsrv\appcmd.exe set app 'Du3Webservice/' /enabledProtocols:"http,net.tcp";

EXPOSE 44318

COPY ${source:-obj/Docker/publish} "C:\Du3Webservice";

#RUN powershell $newCert = (Import-PfxCertificate -FilePath 'C:\Du3Webservice\certs\localhost.pfx' -CertStoreLocation "Cert:\LocalMachine\My");
#RUN powershell $site = (Get-ChildItem -Path "IIS:\\Sites") | where {( $_.Name -eq 'Du3Webservice' )};
#RUN $binding = $site.Bindings.Collection | where {( $_.protocol -eq 'https' -and $_.bindingInformation -eq '*:44318:')};
#RUN $binding.AddSslCertificate((Import-PfxCertificate -FilePath 'C:\Du3Webservice\certs\localhost.pfx' -CertStoreLocation "Cert:\LocalMachine\My").Thumbprint, 'My');


