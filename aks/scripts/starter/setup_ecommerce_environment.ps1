#az login --tenant e80ac3e5-e681-46f9-b730-1c91e4c5b57a
#az account set --subscription cce8043b-d059-4c18-82ac-3a35dc504a2c

param (
    [bool]$Create=$true
)


$envRG = 'ecommerce-env-rg'
$envRGLocation = "westus"
$clusterName = "ecommerce-env-aks-cluster"

if ($Create) {
     Write-Host "Creating AKS Cluster..."
    az group create --name $envRG  --location $envRGLocation
    az provider register --namespace Microsoft.Insights --wait
    az provider register --namespace Microsoft.OperationalInsights --wait
    az provider register --namespace Microsoft.ContainerService --wait
    az provider register --namespace Microsoft.Network --wait
    az provider register --namespace Microsoft.Compute --wait
    az provider register --namespace Microsoft.OperationsManagement --wait
    az provider register --namespace Microsoft.Authorization --wait
    az provider register --namespace Microsoft.Storage --wait;
    az aks create --resource-group $envRG --name $clusterName --node-count 1 --node-vm-size Standard_B2s --enable-addons monitoring --generate-ssh-keys

} else {
    Write-Host "Deleting AKS Cluster..."
    az group delete --name $envRG --yes
}


# az vm list-sizes --location "westus"

