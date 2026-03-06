# Gestion des secrets (PayTranch)

Comme en Django avec `.env`, les variables sensibles ne doivent **jamais** être commitées sur GitHub. Voici comment faire en ASP.NET Core.

---

## En développement (local)

Utilise **User Secrets** : les valeurs sont stockées **hors du projet** (dans ton profil utilisateur), jamais dans le repo.

### 1. Initialiser (déjà fait si le projet a `UserSecretsId` dans le .csproj)

```bash
cd PayTranch
dotnet user-secrets init
```

### 2. Définir tes secrets (équivalent de ton .env Django)

```bash
cd PayTranch

# Chaîne de connexion PostgreSQL (équivalent DB_* en Django)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=paytranch;Username=postgres;Password=postgres"

# Si tu ajoutes une clé secrète custom plus tard :
# dotnet user-secrets set "AppSettings:SecretKey" "ta-cle-secrete"
```

### 3. Vérifier / lister (sans afficher les valeurs)

```bash
dotnet user-secrets list
```

### 4. Où c’est stocké

- **Windows** : `%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json`
- Ce fichier n’est **jamais** dans le projet ni sur GitHub.

---

## En production (Docker, serveur)

Les secrets viennent des **variables d’environnement**, pas de fichiers versionnés.

### Option A : Variables d’environnement

Sur le serveur ou dans Docker :

```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="Host=db;Port=5432;Database=paytranch;Username=postgres;Password=xxx"
```

Dans ASP.NET, `__` (double underscore) remplace `:` dans les noms de section. Donc :

- `ConnectionStrings:DefaultConnection` → variable d’env : `ConnectionStrings__DefaultConnection`

### Option B : Fichier .env avec Docker Compose

Tu peux garder un fichier `.env` **à la racine du projet, non versionné** (il est dans `.gitignore`) :

```env
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=paytranch;Username=postgres;Password=postgres
```

Puis dans `docker-compose.yaml` :

```yaml
services:
  paytranch:
    env_file:
      - .env
```

Comme `.env` est dans `.gitignore`, il ne part jamais sur GitHub. Chaque environnement (ta machine, le serveur) a son propre `.env` avec les vrais mots de passe.

---

## Récap (équivalent Django)

| Django | ASP.NET Core |
|--------|----------------------|
| `.env` en local (pas sur GitHub) | **User Secrets** en local (hors repo) |
| `os.getenv("DB_PASSWORD")` | `Configuration["ConnectionStrings:DefaultConnection"]` (rempli par User Secrets ou env) |
| `.env` en prod sur le serveur | **Variables d’env** ou fichier **.env** (non versionné) injecté par Docker / l’hôte |
| `DEBUG`, `SECRET_KEY` dans .env | `ASPNETCORE_ENVIRONMENT`, Data Protection (clés auto), ou ta propre clé en User Secrets / env |

---

## Fichiers à ne jamais committer

- `.env`
- `appsettings.Production.json` (si tu y mets des secrets)
- `appsettings.*.local.json`
- Tout fichier contenant des mots de passe ou clés

Le fichier **`.env.example`** à la racine liste uniquement les **noms** des variables (sans valeurs) pour que l’équipe sache quoi configurer.
