Get AKS Credentials: az aks get-credentials --resource-group ecommerce-env-rg --name ecommerce-env-aks-cluster
Get AKS Deployments: kubectl get deployments --namespace ecommerce-namespace
Get AKS Deployments Information: kubectl describe deployment rabbitmq-deployment --namespace ecommerce-namespace

Get AKS PODS: kubectl get pods --namespace ecommerce-namespace
GET AKS PODS Description: kubectl describe pod orders-microservice-deployment-dbc8db88c-pn8h2 --namespace ecommerce-namespace
GET AKS PODS Logs: kubectl logs orders-microservice-deployment-dbc8db88c-pn8h2 --namespace ecommerce-namespace

GET AKS ALL: kubectl get all --namespace ecommerce-namespace
GET AKS Services: kubectl get services --namespace ecommerce-namespace