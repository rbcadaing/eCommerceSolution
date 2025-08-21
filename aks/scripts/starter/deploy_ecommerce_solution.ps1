
param (
    [string]$clusterName="ecommerce-env-aks-cluster",
    [string]$envRG = "ecommerce-env-rg",
    [string]$namespace = "ecommerce-namespace"
)

$envRG = $envRG
$ecommerceClusterName = $clusterName
$ecommerceNameSpace = $namespace

az aks get-credentials --resource-group $envRG --name $ecommerceClusterName
kubectl create namespace $ecommerceNameSpace
kubectl apply -f ../../mongodb-deployment.yaml
kubectl apply -f ../../mysql-deployment.yaml
kubectl apply -f ../../postgres-deployment.yaml
kubectl apply -f ../../rabbitmq.secret.yaml
kubectl apply -f ../../rabbitmq-deployment.yaml
kubectl apply -f ../../redis-deployment.yaml
kubectl apply -f ../../nginx-test-deployment.yaml
kubectl apply -f ../../apigateway-deployment.yaml
kubectl apply -f ../../products-microservice-deployment.yaml
kubectl apply -f ../../users-microservice-deployment.yaml
kubectl apply -f ../../orders-microservice-deployment.yaml







