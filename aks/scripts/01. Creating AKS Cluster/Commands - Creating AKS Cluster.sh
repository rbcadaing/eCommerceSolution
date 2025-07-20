# Login to specific tenant (from Entra ID)
    az login --tenant <TENANT_ID>
    az login --tenant 8ae4526f-98aa-4bcc-b9c8-abd102c7d981

# Create resource group
    az group create --name ecommerce-resource-group --location "South India"

# Create container registry
    az acr create --resource-group ecommerce-resource-group --name harshaecommerceregistry --sku Basic

-------------------

# Register Microsoft.Insights Resource Provider, which is required for Cluster monitoring:
    az provider register --namespace Microsoft.Insights --wait; az provider register --namespace Microsoft.OperationalInsights --wait;  az provider register --namespace Microsoft.ContainerService --wait;  az provider register --namespace Microsoft.Network --wait;  az provider register --namespace Microsoft.Compute --wait;  az provider register --namespace Microsoft.OperationsManagement --wait;  az provider register --namespace Microsoft.Authorization --wait;  az provider register --namespace Microsoft.Storage --wait;

    # Microsoft.Insights: Enables monitoring and diagnostics for Azure resources.
    # Microsoft.OperationalInsights: Supports Log Analytics for operational data monitoring.
    # Microsoft.ContainerService: Manages Azure Kubernetes Service (AKS) and container workloads.
    # Microsoft.Network: Provides network-related services like VNETs, Load Balancers, and VPNs.
    # Microsoft.Compute: Manages virtual machines, scale sets, and related compute resources.
    # Microsoft.OperationsManagement: Handles operations management solutions, like Azure Automation.
    # Microsoft.Authorization: Manages access control (RBAC) and policy settings for resources.
    # Microsoft.Storage: Manages Azure Storage services like blobs, files, and queues.



# Create AKS cluster:
    az aks create --resource-group ecommerce-resource-group --name ecommerce-aks-cluster --node-count 1 --node-vm-size Standard_B2s --enable-addons monitoring --generate-ssh-keys

#   --node-count 1

#       Node Count: Specifies the number of nodes (virtual machines) in your AKS cluster.
#       Nodes: Each node is a VM that runs your Kubernetes workloads (containers).
#       In this command, --node-count 1 means the cluster will have 1 nodes. You can add more; for example "5" if you need more resources in real world projects. You can update it later, if needed.

#   Default: Standard_B2s:
#       vCPUs: 2
#       Memory: 8 GB
#       Temporary Storage: 4 GB
#       Use Case: Cost-effective for burstable workloads where CPU usage is not consistent but needs to handle spikes in demand.

#       Other Options:
#       Standard_DS2_v2: A more powerful VM, suitable for production workloads.
#       Standard_E8s_v3: High memory VMs for memory-intensive application


#   --enable-addons monitoring

#       Enable Addons: This flag is used to enable additional features in your AKS cluster.

#       Monitoring: In this case, monitoring refers to enabling Azure Monitor for containers, which provides insights and monitoring capabilities for your Kubernetes cluster.

#       Even for learning purposes, it's useful to enable monitoring so you can observe the performance and behavior of your applications and the Kubernetes cluster itself.


#   --enable-managed-identity: 
#       This enables a system-assigned managed identity for the AKS cluster, which is useful for securely accessing Azure resources in your AKS cluster.


#   --generate-ssh-keys

#       SSH Keys: Secure Shell (SSH) keys are used to securely access the virtual machines (nodes) in your cluster.

#       If you need to troubleshoot or manage the individual nodes (VMs), SSH keys allow you to securely connect to them.

#       SSH keys are a way to securely access remote nodes (VMs) without needing to type in a password each time. They work using a pair of keys:   

#           Public Key: This key is placed on the remote server you want to access. It's like leaving a padlock on the server's door.   
#           Private Key: This key stays on your computer and is kept secret. It's like the key to the padlock you left on the server.

#       When you want to connect, your computer uses your private key to prove it has the right to unlock the padlock (the public key) on the server. This allows you to log in to the node (VM) without a password.   



# Enable "kubectl" command:
    az aks get-credentials --resource-group ecommerce-resource-group --name ecommerce-aks-cluster

#   This command fetches the credentials and configuration details for your AKS cluster and updates your local kubeconfig file. 
#   It allows your local machine to connect to the AKS cluster and use kubectl to manage it. 
#   By running this, you enable your local Kubernetes client to interact with your cluster, making it possible to deploy and manage applications from your local setup.


# Get cluster info:
    kubectl cluster-info
    
#   This command provides information about the Kubernetes cluster you are currently connected to. 


# kubectl Quick Reference: https://kubernetes.io/docs/reference/kubectl/quick-reference/

    kubectl [or] kubectl --help

# Get current cluster name [Optional]:
    kubectl config current-context

