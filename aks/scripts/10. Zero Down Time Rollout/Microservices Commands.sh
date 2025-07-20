az login

az acr login --name harshaecommerceregistry

# [Already Done] Attach ACR to the cluster (Grant the AKS managed identity the AcrPull role on the ACR):
    az aks update --resource-group ecommerce-resource-group --name demo-aks-cluster --attach-acr harshaecommerceregistry

# --------------

# Namespace:
    kubectl create namespace ecommerce-namespace

# --------------

# Apply all in the current folder:
    kubectl apply -f .
    kubectl delete -f .


# Apply the deployment yaml file:
    kubectl apply -f mysql-deployment.yaml
    kubectl apply -f mongodb-deployment.yaml
    kubectl apply -f postgres-deployment.yaml
    kubectl apply -f users-microservice-deployment.yaml
    kubectl apply -f products-microservice-deployment.yaml
    kubectl apply -f redis-deployment.yaml
    kubectl apply -f rabbitmq-deployment.yaml
    kubectl apply -f apigateway-deployment.yaml
    kubectl apply -f orders-microservice-deployment.yaml

# ---------------

    kubectl apply -f mysql.service.yaml
    kubectl apply -f mongodb.service.yaml
    kubectl apply -f postgres.service.yaml
    kubectl apply -f users-microservice.service.yaml
    kubectl apply -f products-microservice.service.yaml
    kubectl apply -f redis.service.yaml
    kubectl apply -f rabbitmq.service.yaml
    kubectl apply -f apigateway.service.yaml
    kubectl apply -f orders-microservice.service.yaml

# ---------------

# ** Get service details [for external IP]:
    kubectl get service apigateway --namespace ecommerce-namespace


# -----------

    kubectl delete -f mysql-deployment.yaml
    kubectl delete -f mongodb-deployment.yaml
    kubectl delete -f postgres-deployment.yaml
    kubectl delete -f users-microservice-deployment.yaml
    kubectl delete -f products-microservice-deployment.yaml
    kubectl delete -f redis-deployment.yaml
    kubectl delete -f rabbitmq-deployment.yaml
    kubectl delete -f apigateway-deployment.yaml
    kubectl delete -f orders-microservice-deployment.yaml

# ---------------

    kubectl delete -f mysql.service.yaml
    kubectl delete -f mongodb.service.yaml
    kubectl delete -f postgres.service.yaml
    kubectl delete -f users-microservice.service.yaml
    kubectl delete -f products-microservice.service.yaml
    kubectl delete -f redis.service.yaml
    kubectl delete -f rabbitmq.service.yaml
    kubectl delete -f apigateway.service.yaml
    kubectl delete -f orders-microservice.service.yaml


# Delete all pods of a specific deployment:
    kubectl delete pods -l app=mysql --namespace=ecommerce-namespace

# Delete a pod
    kubectl delete pod pod-name --namespace namespace-name


# ---------------

# Get namespaces:
    kubectl get namespaces

# Get deployments in a namespace:
    kubectl get deployments --namespace ecommerce-namespace

# Get all in a namespace:
    kubectl get all --namespace ecommerce-namespace


# --------------


# Get deployment details:
    kubectl describe deployment orders-microservice-deployment --namespace ecommerce-namespace


# ** Get list of pods in a namespace:
    kubectl get pods --namespace ecommerce-namespace

#   We haven't created any pods yet. Pods are the smallest deployable units in Kubernetes.
#   A pod can run one or more containers.


# Get list of pods of a specific deployment:
    kubectl get pods --selector=app=demowebapp --namespace ecommerce-namespace


# ** Get list of containers in a pod - To Troubleshoot a pod:
    kubectl describe pod orders-microservice-deployment-55b9f79877-28r86 --namespace ecommerce-namespace


# ** Get logs of first container of a pod:
    kubectl logs orders-microservice-deployment-55b9f79877-28r86 --namespace ecommerce-namespace


# ** Get live logs of a pod:
    kubectl logs -f apigateway-6459ff494-b4kl5 --namespace ecommerce-namespace


# Get logs of specific container in a pod:
    kubectl logs orders-microservice-deployment-55b9f79877-28r86 -c demowebapp-container --namespace ecommerce-namespace


# ------------



# Get events of a namespace:
    kubectl get events --namespace ecommerce-namespace

# -----------


# Service communication internally:
# If both services are in the same namespace:
    service_name:port

# If not:
    <service-name>.<namespace>.svc.cluster.local:<port>


# -------------


# Delete all deployments:
    kubectl delete deployments --all --namespace=ecommerce-namespace

# Delete all services:
    kubectl delete services --all --namespace=ecommerce-namespace


# ---------------

# Delete a deployment:
    kubectl delete deployment mysql-deployment --namespace ecommerce-namespace


# -----------------
