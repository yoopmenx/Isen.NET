# Prérequis 
* Installer Visual Studio Code
* Installer .Net Core SDK 2.2 :
  https://www.microsoft.com/net/download/core    
# Préparation de la structure de la solution
* `mkdir Isen.DotNet`  
* `cd Isen.DotNet`  
* `git init`  
* `touch .gitignore`  (ou créer un fichier avec ce nom depuis VS Code) puis récupérer un gitignore spécifique à .Net Core.  

Créer un repository sur GitHub / GitLab, puis l'ajouter en temps que
remote sur notre repository local :
`git remote add origin https://github.com/kall2sollies/Isen2019.git`  

Faire un commit initial de nos sources :
`git add .`  
`git commit -m "initial commit"`  
`git push origin master`  

Créer un projet Console, dans un sous-dossier src:
* Créer le dossier src/ et naviguer dedans  
* Dans le dossier src, créer    Isen.DotNet.ConsoleApp et naviguer dedans  
* Créer le projet console : `dotnet new console`  

Créer le fichier solution (.sln) :
* naviguer vers la racine du projet  
* Créer le fichier .sln : `dotnet new sln` 
* Ajouter les différents éléments de la solution à ce projet (projet console):
** `dotnet sln add src/Isen.DotNet.ConsoleApp/`  
Créer un dossier src/Isen.DotNet.Library et naviguer dedans.
Avec la CLI .Net (dont l'interface en ligne de commande, que l'on utilise depuis le début), créer un projet de type 'librairie de classe':
`dotnet new classlib`  

Référencer ce nouveau projet dans le fichier de solution (.sln).
Depuis la racine : `dotnet sln add src/Isen.DotNet.Library`  

Ajouter le projet Library comme référence du projet ConsoleApp:
* Naviguer dans le dossier du projet console  
* `dotnet add reference ../Isen.DotNet.Library`  

# Le C#

## Création d'une classe Hello
Supprimer la classe autogénée (Class1.cs).  
Créer un fichier Hello.cs, et coder la classe.  

## Création d'une class MyCollection
Cette classe aura pour but de manipuler 
une liste de string dans un premier temps.  
* Créer dans le projet Library un 
  sous-dossier Lists,
* une classe MyCollection
* Coder la methode `Add(string item)`  

## Ajouter un projet de tests unitaires
* A la racine de la solution, créer un dossier `tests` et un sous-dossier `Isen.DotNet.Library.Tests` 
* Naviguer vers ce dossier
* `dotnet new xunit`  
* Ajouter ce projet au sln. Depuis la racine: `dotnet sln add tests/Isen.DotNet.Library.Tests`  
* Revenir dans le dossier du projet de test
* Référencer le projet Library dans le projet de test: `dotnet add reference ../../src/Isen.DotNet.Library`  
* Renommer la classe générée automatiquement dans le projet de test et l'appeler `MyCollectionTest`  
* Coder un test de la méthode Add
* Exécuter `dotnet test`  
* Coder les accesseurs indexeurs
* Coder méthodes de tests Count et Index

## Refactoring de la classe MyCollection en classe générique

Réécriture de la classe `MyCollection` qui devient le générique `MyCollection<T>`  

Modification de la classe de test: `new MyCollection()` devient `new MyCollection<string>()`  

Renommer MyCollectionTest.cs en MyCollectionStringTest.cs et renommer la classe de la même façon.

Dupliquer MyCollectionStringTest en MyCollectionCharTest et adapter le code de test en conséquence.

## Implémentation l'interface IList<T>

Indiquer l'implémentation de l'héritage de l'interface IList<T>.
Utiliser l'ampoule de Omnisharp pour :
* générer automatiquement le using manquant
* implémenter les prototypes des méthodes de l'interface

Coder ensuite les méthodes, et leurs tests.

## Manipulation de modèles

### Apartée sur les types nullables
```csharp

// Person est un type référence
Person person; // null
person = new Person(); // pas null
person = null; // re-null

// int est un Value Type
// les types primitifs (bool, int, long, float...)
// sont des types valeur
bool b; //pas null, il vaut sa valeur par défaut (false)
b = true; // true
// b = null; // interdit

// bool? est un bool nullable (type référence)
// bool? != bool
// bool? == Nullable<bool>
bool? nb = null; // null
Nullable<bool> nbb; // null aussi
var hasValue = nb.HasValue; // false
nb = true;
var val = nb.Value; // true

```

Dans le projet Library, créer un dossier Models (au pluriel).  
Créer une classe Person (au singulier).  

### Retour au modèle
Une personne a 3 champs :
* Prénom
* Nom
* Date de naissance optionnelle

On crée 2 constructeurs (2 et 3 params).  
La version 3 param appelle celui à 2 params puis complète.  

Ajouter un accesseur lecteur seule (getter) pour obtenir l'âge.

### Migration vers une entité

Ajouter un champ Id (int) et un champ Name.
Pour le champ Name, on va déclarer explicitement le champ de backup privé _name

### Modele City

Créer dans Models une classe City, avec Id et Name + ZipCode.

Dans Person, ajouter un champ BornIn de type City.  

### Refactoring
Déplacer Id et Name vers une classe de base, abstraite : `BaseModel`. 

Faire dériver City et Person de BaseModel.
Noter les champs Id et Name comme des override (puisque les champs sont virtuels dans BaseModel). 

### Display

Implémenter un mode d'affichage de base (string), overridable, et l'utiliser dans `ToString()`.  

Compléter ce mécanisme afin d'ajouter le ZipCode à l'affichage des City.  

Puis reprendre l'affichage d'une Person.  

# Création de Repositories

Créer cette arbo :
[Libray]
    /Repositories
    /Repositories/Base (repo abstrait)
    /Repositories/Interfaces (base et interfaces spécifiques)
    /Repositories/InMemory (implémentations InMemory)

## Exemple concret sur CityRepository

Sous InMemory, créer la classe `InMemoryCityRepository`.   

Implémenter une liste test (ModelCollection).  

### Single

Ajouter 2 méthodes `Single()` : recherche par Id, et recherche par Name.  

Ecrire des tests unitaires pour tester ces 2 méthodes single.

### Update

Créer une méthode qui permette de renvoyer le premier Id dispo (max + 1).

Créer une méthode Update qui gère automatiquement les créations de nouvelles entités, ou les mises à jour d'entités existantes.

Créer une méthode `SaveChanges()` qui permette un mécanisme de transaction (décider de sauver tous les changements ou non) via une copie du contexte.  

### Delete

Créer une méthode de `Delete()` d'une entité, qui utilise le mécanisme de transaction.  

### Listes

Créer une méthode `GetAll()` qui renvoie toutes les entités du contexte.

Créer une méthode `Find()` qui prend comme paramètre un prédicat de recherche, sous forme de lambda expression (méthode anonyme).

A ce stade, nous avons couvert toutes les opérations de CRUD:
* C = Create  
* R = Read  
* U = Update  
* D = Delete  

## Refactoring 

### Généralisation du repo
Dans le dossier Base, créer `BaseInMemoryRepository`.   
Déplacer toutes les méthodes de CityRepositoty vers BaseInMemoryRepository et les adapter en généric.  

### Extraction d'interface
Dans le dossier Interfaces, créer `IBaseRepository`  et y rappatrier toutes les signatures des opérations de CRUD.  

Créer une interface `ICityRepository`, qui implémente `IBaseRepository`, sans ajouter de méthode.  
`InMemoryCityRepository` devra implémenter cette interface.  

### TD : Refaire la même chose pour PersonRepository
* Créer l'interface IPersonRepository
* Créer InMemoryPersonRepository
* Créer InMemoryPersonRepoTest en dupliquant l'autre

### Composition / injection de repositories

Dans `PersonRepository`, ajouter un constructeur, qui prend comme paramètre une 
interface de `ICityRepository`.  

La classe `PersonRepository` nécessite d'avoir une instance de `ICityRepository` pour fonctionner. On dit qu'elle a une dépendance sur cette classe.

Cette dépendance est déclarée dans son constructeur.

Ce design pattern s'appelle :
* Pattern d'Injection de Dépendance
* également IoC : Inversion of Control
* ou encore DI : Dependency Injection  

### Relations réciproques

Nous avons la classe de modèle `Person` qui a un champ de type `City` dans sa propriété `BornIn`.  

En termes de verbatim OOP, c'est une relation par composition (une classe a un champ dont le type est une autre classe), par opposition à une relation par héritage.

En termes de verbatim de Base de données relationnelle, c'est une relation `one-to-many`, puisque une personne a une ville, mais une ville a potentiellement plusieurs personnes.  

On peut donc, au niveau de `City`, ajouter une propriété de liste de personnes, qui serait donc la relation réciproque de `Person.BornIn`.   

Attention cependant, même si on ajoute cette relation, elle ne va pas se remplir toute seule.  


# Ajout d'un projet ASP.NET MVC (Core)

## Ajout du projet depuis le template de la CLI .Net Core

Depuis le dossier src, ajouter un dossier `Isen.DotNet.Web`. Naviguer dans ce dossier puis `dotnet new mvc`.  
Directement : `dotnet run`  et ouvrir https://localhost:5001.  

Ajouter le projet Library en référence au projet Web :  
`dotnet add reference ../Isen.DotNet.Library/`  

Revenir à la racine, et ajouter ce projet à la solution.  
`dotnet sln add src/Isen.DotNet.Web/`  

## Anatomie d'un projet MVC

Le schéma (routing) par défaut des url d'un projet MVC est:

`https://localhost:5001/[Vue]/[Action][?param=value&param2=value2]`  

OU  

`https://localhost:5001/[Vue]/[Action]/[param/value...]`  

Ce schéma peut être complété / modifié / réécrit selon les besoins.  

* `/wwwroot` : contient essentiellement les assets de l'application, soit les images, les css (scss/sass) et les js (ou typescript), ainsi que les copies locales des librairies utilisées (BootStrap 4, jQuery...). Globalement, tout ce qui doit être chargé côté client, donc côté navigateur.  

* `/Views` : Fichiers `.cshtml`. Ce sont des templates HTML écrits avec la syntaxe de templating *Razor*. Razor utilise +/- la syntaxe du C#.  

  * Chaque dossier (à part Shared) correspond à un contrôleur. Ex : le dossier Home contient les vues accessibles via `https://localhost:5001/Home` et chaque vue a un controller correspondant (que l'on verra plus tard) et on peut avoir plusieurs vues par contrôleur. Ex : `Privacy` et `Index` sont 2 vues / action du contrôleur `Home`.

  * Chaque fichier cshtml correspond à une action. Ex : dans `Home`, le fichier `Privacy.cshtml` corrspond à l'url `https://localhost:5001/Home/Privacy`  

   L'action `Index` est l'action par défaut. Donc si l'url ne précise pas d'action dans ses segments, c'est l'action Index qui est appelée.  

  * `/Shared/_Layout.cshtml` contient le template global, dans lequel les vues vont s'insérer. 

  Les chemins en `~/` correspondent à `/wwwroot`.  

  Les librairies (JS/CSS) sont chargées localement en environnement de dev, et depuis un serveur CDN, en environnement de prod.

  Les CSS sont chargées dans le `<head>` de la page, les JS quant à eux, tout à la fin du `<body>`.  

* `/Controllers` : Classes C# dont le but est
  * Sens server > client : de prendre des data dans un modèle, et les injecter dans la vue/action correspondante.
  * Sens client > server : répondre aux requêtes (GET, POST, etc...)
  * Le nommage des classes de controller est normalisé : `NomDeLaVueController` (Ex : `HomeController`)  
  * Le nommage des méthodes est également normalisé, et correspond aux actions. (Ex : `HomeController.Privacy()` ou `HomeController.Index()`) 

* `/Models` : ce dossier contient des ViewModels. Donc des classes C# uniquement consituées de champs (pas de logique, pas de méthodes). On appelle ce genre de classes des POCO (Plain Old C# Object), ou encore des Value Objects.

    Par opposition aux classes de Model métier (Person, City), les ViewModel ont pour but d'avoir uniquement les champs strictement nécessaires à l'affichage. 

    Ex : si on affiche une liste des `Person`, avec uniquement nom et prénom, on créera un `PersonViewModel` avec `First` et `Last`, mais on ne mettra pas `BornIn` ni `DateOfBirth`. Le but étant d'avoir un objet aussi léger que possible, pour minimiser les transferts.  

* `/Startup.cs` : Configuration des injections de dépendances, des services utilisés par l'application (repositories, librariries, loggers, ...)

* `/Program.cs` : Point d'entrée de l'application. Depuis .Net Core et ASP.NET MVC Core, une application web est en fait une application console.

  Ce point d'entrée lance la configuration des services, puis lance le serveur web embarqué (Kestrel).  

* `/appsettings.json` : ce sont les settings de l'application. La chaîne de connexion à une base de données se retrouvera là-dedans.  

# Ajout de vues

## Ajout des éléments de menu

Le menu de navigation utilise les éléments et classes issus du framework Bootstrap 4. Utiliser les éléments de ce framework pour ajouter une navigation en menus déroulants.
Doc : https://getbootstrap.com/docs/4.1/getting-started/introduction/
Menu + dropdown : https://getbootstrap.com/docs/4.1/components/navs/#tabs-with-dropdowns

Ajouter la navigation suivante dans le fichier _Layout.cshtml :
* Villes (pas une page, juste un noeud de menu)
  * Toutes (afficher la liste des villes)  
    `https://localhost:5001/City/[Index]`
  * Ajouter... (afficher un formulaire de saisie vide)  
    `https://localhost:5001/City/Edit/`

Lorsqu'on cliquera sur une ville dans la liste, 
on pourra l'éditer avec une url du type 
`https://localhost:5001/City/Edit/2` qui appellera 
en fait la vue "Ajouter", 
qui servira aussi bien à la création qu'à la modification.  

## Ajouter le contrôleur CityController

Dans le dossier des contrôleurs, 
créer `CityController.cs` et ajouter les méthodes 
correspondant aux 2 actions prévues (Index, Edit).  

## Ajouter les vues correspondantes

### Vues vides

Dupliquer le dossier Home et le nommer `City`, 
vider et adapter les vues : enlever tout le html 
existant, et mettre juste un titre dans chaque vue.  

### Maquette du tableau
Utiliser les éléments de tableaux en layout Bootstrap 
issus de cette doc : 
https://getbootstrap.com/docs/4.1/content/tables/ 
Ex : Striped rows

### Injection d'un modèle dans la vue liste / tableau

Dans `CityController`, instantier un `ICityRepository`, de type concret
`InMemoryCityRepository`. Récupérer la liste des villes, et la passer à la 
vue.  

Dans la vue `/Views/City/Index.cshtml`, préciser le type du modèle en syntaxe Razor : 
`IEnumerable<City>`, avec les directives `@using` et `@model`.  

Itérer le bloc html `<tr>...</tr>' avec une directive `@foreach`. Puis 
remplacer les valeurs hard-codées du tableau, par les champs de la variable city.

Ajouter les attributs de construction d'URL sur les 2 liens `<a>` (Modifier,
supprimer)

### Construction de la vue d'édition

Dans `/Views/City/Edit.cshtml`, ajouter 2 champs texte pour `City.Name`
et `City.ZipCode` et 2 boutons (validation, annulation).  

Dans le contrôleur, injecter les données (la ville sélectctionnée) dans l'action
Edit.  

Dans la vue, lier les données du modèle aux champs du formulaire.  

Dans le contrôleur, ajouter une 2ème action Edit, en `HTTP POST`. Cette action
prend un `City` en paramètre, et cette instance de `City` correspondra aux
données saisies dans les champs du formulaire. 

Dans la vue Edit, ajouter un champ caché, qui permettra de conserver la valeur
de l'id entre l'affichage du formulaire, et son post.  

### Utilisation du repository en injection de dépendances.

Dans le contrôleur, réécrire le constructeur de façon à ce qu'il ait besoin
d'un `ICityRepository` pour pouvoir s'exécuter. Le contrôleur, au travers de
son constructeur, va explicitement exprimer sa (ou ses) dépendances :
j'ai besoin d'un `ICityRepository` pour fonctionner.  

L'instanciation du `InMemoryCityRepository` a maintenanant disparu du
constructeur, il faut donc indiquer quelque part que lorsqu'un constructeur
requiert un `ICityRepository`, alors, on doit lui fournir un 
`InMemoryCityRepository`.

Ceci est précisé dans `Startup.cs`:

```csharp
// AddScoped : nouvelle instance à chaque requête HTTP, 
// Partage de la même instance si on y fait au sein de la même requête
services.AddScoped<ICityRepository, InMemoryCityRepository>();

// AddSingleton : instance unique, créée au premier appel,
// et donc partagée d'une requête HTTP à l'autre, pendant tout le cycle
// de vie du serveur.
services.AddSingleton<ICityRepository, InMemoryCityRepository>();

// AddTransient : nouvelle instance à tout appel, et même au sein de
// la même requête HTTP
services.AddTransient<ICityRepository, InMemoryCityRepository>();
```

### Application de l'ensemble de la chaîne au modèle `Person`

Echauffauder (scadffolfing) le même principe pour avoir 2 formulaires d'édition
de personne :
* Ajouter une entrée au menu
* Ajouter un contrôleur `PersonController`
* Ajouter la vue `Index` avec tableau des personnes. Ne pas tenir compte du 
  champ city
* Ajouter la vue `Edit` avec formulaire d'édition de `Person`.  Ne pas tenir 
  compte du champ city
* Implémenter la suppression depuis la liste

Ajouter le champ `Person.BorIn (City)` dans le formulaire d'édition.
Ce champ sera une liste déroulante contenant toutes les villes.

### Refactoring

On se rend compte que le code des 2 contrôleurs est très similaire, puisque
seul le nom des classes change.

Créer un `BaseController`, modifier l'héritage des 2 contrôleurs existants, 
puis basculer dans `BaseController` tout ce qui peut être mutualisé.

Rendre `BaseController` générique, en lui permettant d'accepter un type de 
modèle, et un type d'interface de repository:

```csharp
public abstract class BaseController<T, TRepo> : Controller
    where T : BaseModel<T>
    where TRepo : IBaseRepository<T>
```

Mutaliser le constructeur et la variable membre de repository.

Mutualiser les méthodes Index, Edit, Edit (post), Delete

## Utilisation d'une base de données

Base de donnée utilisée : `Sqlite`  
Framework ORM : Entity Framework (EF)

### Ajouter au projet les références nécessaires

En CLI : naviguer vers le dossier du projet Library, puis
`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`  
`dotnet add package Microsoft.EntityFrameworkCore.Design`

### Chaine de connexion
Dans `appsettings.json` (racine du projet web), ajouter :
```
"ConnectionStrings" : {
    "DefaultConnection" :  "DataSource=.\\IsenWeb.db" 
  },
```

### Création du contexte

Dans le modèle Person, ajouter un champ `BornInId`.

Dans le projet Library, ajouter un dossier `Context`
puis une classe `ApplicationDbContext` et qui 
dérive de `DbContext`.

Ajouter les `DbSet<>` correspondant aux classes du modèle.  
Implémenter le constructeur avec options, et appeller `base()`.  
Surcharger `OnModelConfiguring()` et préciser les tables
et relations.  

Préciser dans `Startup` que l'on utilise Entity Framework avec
une base de données Sqlite.  

### Création de repositories basés sur EF

Actuellement :
* `IBaseRepository` : signatures CRUD + accès à un contexte
* `BaseInMemoryRespository` : Accès à un contexte Mock (en mémoire) 

Créer une classe `BaseDbRepository` qui implémente 
`IBaseRepository`.  

Ajouter un champ `DbContext`, permettant d'accéder au contexte
EF.  

Implémnenter les méthodes de l'interface, et les reprendre
depuis la version InMemory, en adaptant au fait d'utiliser
`DbContext`.  

Créer `DbContextCityRepository` et `DbContextPersonRepository`
en dérivant de `BaseDbContextRepository` et en implémentant 
les interfaces correspondantes (`ICityRepository` et 
`IPersonRepository`).  

Dans `Startup`, remplacer le mapping d'injection de dépendances
de cette façon :

```csharp
services.AddScoped<ICityRepository, DbContextCityRepository>();
services.AddScoped<IPersonRepository, DbContextPersonRepository>();
```

### Initialisation de la base de données

Dans le projet Library, dossier Context, créer une classe
`SeedData`.  
Par injection de dépendances, permettre l'accès au contexte
et aux 2 repositories.  

Ajouter une fonction pour détruire / recréer la bdd.  

Ajouter 2 méthodes pour :
* Créer les villes
* Créer les personnes, en leur affectant des villes

Sur les classes City et Person, ajouter l'attribut `[NotMapped]` 
sur les champs qui ne doivent pas être en base de données.

### Appel de l'initialisation

Ajouter `SeedData` au conteneur Ioc 
(injection de dépendances) :
```csharp
services.AddScoped<SeedData>();
```

Dans Program.cs, récupérer une instance de la 
collection des services injectés, afin de récupérer 
un `SeedData`, et appeler les méthodes d'initialisation 
de la bdd.  

### Ajout des inclusions de données avec EF

Dans `BaseDbRepositories`, ajouter une méthode virtuelle
permettant de préciser quelle(s) relation(s) doivent être
requêtées.

Dans `BaseDbRepositories`, appeler cette méthode dans
Single, GetAll et Find.  

Dans `DbContextPersonRepository`, surcharger la méthode
`Includes` afin de demander le chargement de la relation
`Person.BornIn`.  

Corriger le `Edit` de la vue Person afin qu'il cible le champ
`Person.BornInId`.  

Dans `DbContextCityRepository`, surcharger Inlcudes afin 
d'inclure la relation `City.PersonCollection`.  
Dans la vue `City/Index`, ajouter une colonne qui indique
le nombre de personnes dans la ville.  

## Création d'une API REST

### Test 

Dans `BaseController`, ajouter une méthode `Status`
et qui aura comme route `/api/{controller}/status`.  
Cette méthode instancie un objet `dynamic` par l'intermédiaire
de la classe `ExpandoObject` et le renvoie après l'avoir
sérialisé en JSON.  

### Mise en place d'un mécanisme de sérialisation avec surcharges

Dans `BaseModel`, créer une méthode `ToDynamic()` qui 
convertit l'instance en type dynamique, avec ses champs 
de base.  

### API : méthode unitaire

Dans le `BaseController`, section API, ajouter une méthode
`GetById`, et dont la route sera :
`/api/[controller]/{id}.
Cette méthode récupère l'objet ayant cet id, appelle 
`ToDynamic()`, et sérialise en JSON.
Ex : `/api/city/2`.  

### API : méthode de liste

Dans une API REST, `/api/city` devrait renvoyer la liste de
tous les `City`.  Ajouter une méthode `GetAll` qui remplisse
ce rôle.  

### Enrichir les objets Person et City

Surcharger `Person.ToDynamic()` afin d'ajouter FirstName,
LastName, DateOfBirth et Age.

De la même façon, ajouter à City le nombre de personnes. 

### Imbrication

Surcharger de nouveau le JSON de Person, afin d'inclure
l'objet City entier, lui aussi sérialisé.   