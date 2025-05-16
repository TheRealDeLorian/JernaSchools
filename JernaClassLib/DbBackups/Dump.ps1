[cmdletbinding()]
param(
    [string]$server="localhost",
    [int]$port=5432,
    [string]$database="mydb",
    [parameter(Mandatory=$true)]
    [string]$user,
    [string]$password,
    [string]$localFolder=".",
    [string]$backupName=((get-date).ToString("yyyyMMdd.hh.mm.ss")+".sql"),
    [string]$schema="public"
)
if($password -eq $null) {
    $ss = read-host "Enter your password" -AsSecureString
    $password = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
}
pg_dump -h $server -p 5432 -U $user -f $backupName -d $database --schema=$schema 
#docker run --rm -e PGPASSWORD=$password -v "$($localFolder):/usr/backupoutput" -it postgres pg_dump -h $server -p 5432 -U $user -f /usr/backupoutput/$backupName -d $database --schema=$schema 