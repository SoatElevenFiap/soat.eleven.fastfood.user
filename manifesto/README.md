## Passos para Inicialização do Cluster com Fastfood

### Pré-requisitos

- GO go.dev
- KIND instalado (`go install sigs.k8s.io/kind@latest`)
- `kubectl` instalado
- Docker Desktop
- PowerShell (para uso de alias)

---

**⚠️ Importante:** 
- Certifique-se de adicionar `127.0.0.1 fastfood` ao arquivo hosts do Windows
- Caminho: `C:\Windows\System32\drivers\etc\hosts`

## 🚀 Guia de Deploy Passo a Passo

### **ETAPA 1: Preparação das Imagens Docker** 🐳

Primeiro, construa as imagens Docker necessárias:

```bash
# 1.1 - Build da imagem do banco de dados (PostgreSQL com migrations)
docker build -t localhost/fastfood-db:latest ./src/Soat.Eleven.FastFood.User.Infra/

# 1.2 - Build da imagem do migrator (para executar migrations)
docker build --target migrator -t acrfastfoodpostech.azurecr.io/fastfood/migrator:v1 .

# 1.3 - Build da imagem da aplicação principal
docker build --target final -t localhost/fastfood-app:latest .
```

### **ETAPA 2: Exportação das Imagens para KIND** 📦

Exporte as imagens para arquivos `.tar` (necessário para KIND):

```bash
# 2.1 - Exportar imagens para formato .tar

docker save -o fastfood-app.tar localhost/fastfood-app:latest
docker save -o fastfood-db.tar localhost/fastfood-db:latest
docker save -o fastfood-migrator.tar localhost/fastfood-migrator:latest
```

### **ETAPA 3: Criação do Cluster KIND** ⚙️

```bash
# 3.1 - Criar cluster KIND com configuração personalizada
kind create cluster --name fastfood-cluster --config ./manifesto/kind-config.yaml

# 3.2 - Carregar imagens no cluster (IMPORTANTE: Faça na ordem!)
kind load image-archive fastfood-db.tar --name fastfood-cluster
kind load image-archive fastfood-migrator.tar --name fastfood-cluster
kind load image-archive fastfood-app.tar --name fastfood-cluster

# 3.3 - Configurar alias para kubectl (opcional, mas recomendado)
Set-Alias -Name k -Value kubectl
```

### **ETAPA 4: Deploy da Infraestrutura Base** 🏗️

```bash
# 4.1 - Deploy do Metrics Server (necessário para HPA)
k apply -f ./manifesto/metrics-server-kind.yaml

# 4.2 - Deploy do Ingress Controller NGINX
k apply -f ./manifesto/fastfood-ingress-80.yaml

# 4.3 - Aguardar Ingress Controller estar pronto
k get pods -n ingress-nginx -w
# ⏳ Aguarde todos os pods ficarem "Running" antes de continuar (Ctrl+C para sair)
```

### **ETAPA 5: Deploy do Namespace e Configurações** 📋

```bash
# 5.1 - Criar namespace da aplicação
k apply -f ./manifesto/fastfood-namespace.yaml

# 5.2 - Deploy das configurações (secrets e configmaps)
k apply -f ./manifesto/secret.yaml
k apply -f ./manifesto/config-map.yaml
```

### **ETAPA 6: Deploy do Banco de Dados** 🗄️

```bash
# 6.1 - Deploy do volume persistente

k apply -f ./manifesto/db-pv.yaml

# 6.1 - Deploy do volume persistente
k apply -f ./manifesto/db-pvc.yaml

# 6.2 - Deploy do serviço do banco
k apply -f ./manifesto/db-service.yaml

# 6.3 - Deploy do banco de dados
k apply -f ./manifesto/db.yaml
```

### **ETAPA 7: Execução das Migrations** 🔄

```bash
# 7.1 - Deploy do job de migração
k apply -f ./manifesto/migrator-job.yaml

# 7.2 - IMPORTANTE: Aguardar conclusão das migrations

```bash
# 4.1 - Deploy do Metrics Server (necessário para HPA)
k apply -f ./manifesto/metrics-server-kind.yaml

# 4.2 - Deploy do Ingress Controller NGINX
k apply -f ./manifesto/fastfood-ingress-80.yaml

# 4.3 - Aguardar Ingress Controller estar pronto
k get pods -n ingress-nginx -w
# ⏳ Aguarde todos os pods ficarem "Running" antes de continuar (Ctrl+C para sair)
```

### **ETAPA 5: Deploy do Namespace e Configurações** 📋

```bash
# 5.1 - Criar namespace da aplicação
k apply -f ./manifesto/fastfood-namespace.yaml

# 5.2 - Deploy das configurações (secrets e configmaps)
k apply -f ./manifesto/secret.yaml
k apply -f ./manifesto/config-map.yaml
```

### **ETAPA 6: Deploy do Banco de Dados** 🗄️

```bash
# 6.1 - Deploy do volume persistente

k apply -f ./manifesto/db-pv.yaml

# 6.1 - Deploy do volume persistente
k apply -f ./manifesto/db-pvc.yaml

# 6.2 - Deploy do serviço do banco
k apply -f ./manifesto/db-service.yaml

# 6.3 - Deploy do banco de dados
k apply -f ./manifesto/db.yaml
```

### **ETAPA 7: Execução das Migrations** 🔄

```bash
# 7.1 - Deploy do job de migração
k apply -f ./manifesto/migrator-job.yaml

# 7.2 - IMPORTANTE: Aguardar conclusão das migrations
k get pods -n fastfood -w
# ⏳ Aguarde o pod "migrator-xxxxx" ficar "Completed" antes de continuar

```

### **ETAPA 8: Deploy da Aplicação Principal** 🚀

```bash
# 8.1 - Deploy do serviço da aplicação
k apply -f ./manifesto/fastfood-service.yaml

# 8.2 - Deploy do ingress para acesso externo
k apply -f ./manifesto/fastfood-ingress.yaml

# 8.3 - Deploy da aplicação principal
k apply -f ./manifesto/fastfood.yaml

# 8.4 - Deploy do auto-scaling (HPA)
k apply -f ./manifesto/fastfood-hpa.yaml

# 8.5 - Verificar se todos os pods estão rodando

```bash
# 4.1 - Deploy do Metrics Server (necessário para HPA)
k apply -f ./manifesto/metrics-server-kind.yaml

# 4.2 - Deploy do Ingress Controller NGINX
k apply -f ./manifesto/fastfood-ingress-80.yaml

# 4.3 - Aguardar Ingress Controller estar pronto
k get pods -n ingress-nginx -w
# ⏳ Aguarde todos os pods ficarem "Running" antes de continuar (Ctrl+C para sair)
```

### **ETAPA 5: Deploy do Namespace e Configurações** 📋

```bash
# 5.1 - Criar namespace da aplicação
k apply -f ./manifesto/fastfood-namespace.yaml

# 5.2 - Deploy das configurações (secrets e configmaps)
k apply -f ./manifesto/secret.yaml
k apply -f ./manifesto/config-map.yaml
```

### **ETAPA 6: Deploy do Banco de Dados** 🗄️

```bash
# 6.1 - Deploy do volume persistente

k apply -f ./manifesto/db-pv.yaml

# 6.1 - Deploy do volume persistente
k apply -f ./manifesto/db-pvc.yaml

# 6.2 - Deploy do serviço do banco
k apply -f ./manifesto/db-service.yaml

# 6.3 - Deploy do banco de dados
k apply -f ./manifesto/db.yaml
```

### **ETAPA 7: Execução das Migrations** 🔄

```bash
# 7.1 - Deploy do job de migração
k apply -f ./manifesto/migrator-job.yaml

# 7.2 - IMPORTANTE: Aguardar conclusão das migrations
k get pods -n fastfood -w
# ⏳ Aguarde o pod "migrator-xxxxx" ficar "Completed" antes de continuar

```

### **ETAPA 8: Deploy da Aplicação Principal** 🚀

```bash
# 8.1 - Deploy do serviço da aplicação
k apply -f ./manifesto/fastfood-service.yaml

# 8.2 - Deploy do ingress para acesso externo
k apply -f ./manifesto/fastfood-ingress.yaml

# 8.3 - Deploy da aplicação principal
k apply -f ./manifesto/fastfood.yaml

# 8.4 - Deploy do auto-scaling (HPA)
k apply -f ./manifesto/fastfood-hpa.yaml

# 8.5 - Verificar se todos os pods estão rodando
k get pods -n fastfood -w
# ⏳ Aguarde todos os pods ficarem "Running"
```

```


```

### **ETAPA 8: Deploy da Aplicação Principal** 🚀

```bash
# 8.1 - Deploy do serviço da aplicação
k apply -f ./manifesto/fastfood-service.yaml

# 8.2 - Deploy do ingress para acesso externo
k apply -f ./manifesto/fastfood-ingress.yaml

# 8.3 - Deploy da aplicação principal
k apply -f ./manifesto/fastfood.yaml

# 8.4 - Deploy do auto-scaling (HPA)
k apply -f ./manifesto/fastfood-hpa.yaml

# 8.5 - Verificar se todos os pods estão rodando
k get pods -n fastfood -w
# ⏳ Aguarde todos os pods ficarem "Running"
```

### **ETAPA 9: Verificação e Acesso** ✅

```bash
# 9.1 - Verificar status geral
k get all -n fastfood

# 9.2 - Verificar HPA
k get hpa -n fastfood

# 9.3 - Verificar ingress
k get ingress -n fastfood
```

**🌐 Acesse a aplicação:**
- **Swagger UI:** http://fastfood/swagger
- **API Base:** http://fastfood/

---

## 📝 Comandos Alternativos (sem alias)

Se não conseguir usar o alias `k`, use os comandos completos com `kubectl`:

```bash
# Infraestrutura
kubectl apply -f ./manifesto/metrics-server-kind.yaml
kubectl apply -f ./manifesto/fastfood-ingress-80.yaml
kubectl get pods -n ingress-nginx -w

# Configurações
kubectl apply -f ./manifesto/fastfood-namespace.yaml
kubectl apply -f ./manifesto/secret.yaml
kubectl apply -f ./manifesto/config-map.yaml

# Banco de Dados
kubectl apply -f ./manifesto/db-pvc.yaml
kubectl apply -f ./manifesto/db-service.yaml
kubectl apply -f ./manifesto/db.yaml

# Migrations
kubectl apply -f ./manifesto/migrator-job.yaml
kubectl get pods -n fastfood -w
# ⏳ Aguarde o migrator completar

kubectl apply -f ./manifesto/fastfood-service.yaml
kubectl apply -f ./manifesto/fastfood-ingress.yaml
kubectl apply -f ./manifesto/fastfood.yaml
kubectl apply -f ./manifesto/fastfood-hpa.yaml
```

---

## 🔍 Troubleshooting

### Verificar Status dos Componentes

```bash
# Verificar todos os recursos
kubectl get all -n fastfood

# Verificar logs da aplicação
kubectl logs -n fastfood -l app=app --tail=50

# Verificar logs do banco
kubectl logs -n fastfood -l app=db --tail=50

# Verificar eventos
kubectl get events -n fastfood --sort-by='.lastTimestamp'

# Verificar se as imagens foram carregadas corretamente
docker exec -it fastfood-cluster-control-plane crictl images | grep fastfood
```

### Problemas Comuns

1. **Pod ficando em "Pending":**
   ```bash
   kubectl describe pod <pod-name> -n fastfood
   ```

2. **Migrations falhando:**
   ```bash
   kubectl logs -n fastfood -l app=migrator
   kubectl describe job migrator -n fastfood
   ```

3. **Aplicação não respondendo:**
   ```bash
   kubectl port-forward -n fastfood svc/app-service 8080:80
   # Teste: http://localhost:8080/health
   ```

4. **Problemas com Ingress:**
   ```bash
   kubectl get ingress -n fastfood
   kubectl describe ingress fastfood-ingress -n fastfood
   ```

---

## 🧹 Limpeza do Ambiente

```bash
# Remover completamente o cluster KIND
kind delete cluster --name fastfood-cluster

# Remover imagens Docker locais (opcional)
docker rmi localhost/fastfood-app:latest
docker rmi localhost/fastfood-db:latest  
docker rmi localhost/fastfood-migrator:latest

# Remover arquivos .tar (opcional)
rm fastfood-app.tar fastfood-db.tar fastfood-migrator.tar
```

---

## 📚 Informações Adicionais

- **Namespace:** `fastfood`
- **Banco de Dados:** PostgreSQL (porta 5432)
- **Usuário DB:** `admin` / Senha: `admin123`
- **URL da Aplicação:** http://fastfood
- **Swagger:** http://fastfood/swagger
- **Health Check:** http://fastfood/health
