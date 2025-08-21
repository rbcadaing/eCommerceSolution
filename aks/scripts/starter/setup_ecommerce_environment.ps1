
param (
    [bool]$Create=$true,
    [string]$clusterName="ecommerce-env-aks-cluster",
    [string]$envRG = "ecommerce-env-rg",
    [string]$envRGLocation = "westus"
)

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

