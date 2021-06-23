# Dark Mario

## Cadre du projet

Dans le cadre de l'unité **Infographie 3D (Unity I)**, dispensée en 1ère année de cycle ingénieurs à l'ESIEE Paris en filière **Informatique et applications**, nous avons été amené à développer le jeu **Dark Mario**.

|                |Deadline      |Etat         | Version|
|----------------|------------- |-------------|--------|
|Début du projet |31 mai 2021   |Réalisé      |-       |
|Idée du projet  |4 juin 2021   |Réalisé      |2.0     |
|Rendu du projet |23 juin 2021  |Réalisé      |1.0     |

## Membres du projet

- Lucas BILLARD : Gameplay Developer / Game & Level Designer ;

	> Gestion du mouvement / des collisions / des attaques des ennemis ;
    
    > Gestion du système de sauvegarde ;
    
    > Création d'un niveau de jeu de difficulté alternative ;
    
- Ewen BOUQUET : Producer / Graphiste 2D & 3D / Music & Sound Designer

	> Création de l'ensemble des animations du projet ;
    
    > Recherche des musique et des sons du projet ;
    
    > Implémentation de l'`AudioManager` ;
    
    > Gestion de l'UI ;
    
    > Gestion du système d'évènements et de boucle du jeu globale ;
    
- Fabien COURTOIS : Gameplay Developer / Sound Designer

	> Gestion des déplacements / sauts / collisions du `Player` ;
    
    > Mise en place du gameplay au niveau des combats et d'armes ;
    
    > Gestion des pics / du dragon ;
    
    > Aide au choix des musiques et des sons du projet ;
    
    > Réalisation d'un niveau de difficulté difficile ;
    
- Loic FOURNIER : 2D & 3D Gfx Artist / Game & Level Designer

	> Réalisation de l'ensemble des modèles 3D via MagicaVoxel ; 
    
    > Gestion des lumières, de l'assemblage des blocs décoratifs du jeu, des particules et plus largement le l'intégrité visuelle du jeu ;
    
    > Réalisation d'un niveau de difficulté facile ;

## Description du projet

Dark est un **jeu de plateforme** inspiré des univers de Mario bros, de Dark Souls et de Castlevania. Le design repose sur du **Voxel** (cf MagicaVoxel). 

Il s'agit en fait d'un jeu en **3D** dans lequel le joueur incarne **un chevalier** qui doit parcourir **différents univers**. Durant son périple, il doit **se battre** avec des ennemis et être abile afin de gravir différents **obstacles**.

L'objectif du jeu est d'atteindre la **porte finale** de fin de jeu avant de **3 minutes 30** sans **mourrir**. Vous pouvez augmenter votre **nombre de points** en tuant un maximum d'ennemis.

## Contrôles du jeu

Il est possible de jouer au jeu de 2 manières :
- Au clavier ;
- Au gamepad type Xbox 360 / Xbox One ;

### Clavier 

- Mouvement gauche-droite : **Q** et **D** ;
- Sauter : **Espace** ;
- Attaque simple : **Clic gauche** ;
- Attaque puissante : **Clic droit** ;
- Levé du bouclier / Parer : **Clic molette** ;

### Manette

- Mouvement gauche-droite : **Joystick gauche** ;
- Sauter : **A** ;
- Attaque simple : **B** ;
- Attaque puissante : **X** ;
- Levé du bouclier / Parer : **Y** ;

## Elements de gameplay

## Victoire / défaite

### Condition de victoire

Le joueur gagne s'il arrive à franchir la **porte de fin de niveau**.

### Condition de défaite

Le joueur perd si 
- Il **meurt** (tué par un ennemi ou par les pics) ;
- Le temps de jeu dépasse **2 minutes 30** ;

### Joueur

- **Player**

	> Le joueur incarne un **chevalier** charismatique et courageux, armé d'une **épée** tranchante  et d'un **bouclier** incroyablement résistant. Son **armure** le protège des ennemis environnants, qu'il pourra exterminer grâce à ses différentes **attaques**. 
    
	> Le `Player` peut réaliser **deux types d'attaques** : une attaque simple, rapide mais qui fait peu de dégâts, et une attaque plus longue, qui donnera du fil a retordre à ses ennemis !
    
    > Le `Player` peut **parer** les attaques des ennemis. Il se cache ainsi derrière son bouclier, et **annule les dégats** de l'attaque bloquée, à condition d'être réalisée au bon moment !
    
    > Le `Player` peut **se déplacer** de droite à gaucher, et **sauter**  afin de franchir divers obstacles.

### Ennemis

- **AxeMan**

	> Le `AxeMan` est un ennemi qui se bat au corps à corps avec une **hâche**, il ne pas sous-estimer ! Il faut vite s'en éloigner après une attaque, car il fait **beaucoup de dégats**... Cependant il peut être tué d'un coup grace à l'attaque puissante du `Player`.
    
- **SpearLauncher**

	> Ce redoutable ennemi est **lancier à distance**, qui va vous gêner dans tous vos déplacements, tel un moustique en pleine nuit d'été. Ses attaques peuvent être bloquées facilement avec le bouclier, mais attention, il est plus **résistant** et donc plus difficile à tuer !
    
- **Dragon**

	> Le `Dragon` est un monstre tant impressionant que majestueux. Il est totalement **invulnérable**, donc n'essayez pas de l'attaquer (c'est peine perdue...) ! Cependant, vous pouvez vous en servir comme allié, car son **crachat de feu** détruit tout ce qu'il touche. Il tue en particulier tous les ennemis sur son passage. Cependant, ne prenez pas trop de risques en vous approchant trop du `Dragon`, où vous finirez totalement **carbonisé**.... :'(

### Elements spéciaux

- **Potion de vie**

	> Cette potion magique, d'origine inconnue, possède des **vertues magiques** tout a fait remarquables. Etant donnée que vous n'êtes pas tombés dedans étant petit, vous pouvez donc la consommer (sans modération). Cette dernière restaurera instantanément **une partie de votre vie**, ce qui est indispensable pour finir certains niveaux.
    
- **Pics**

	> Ces obstacles **pointus** vous donneront du fil à retordre, car ils vous **blesseront** à leur contact. Soyez malin dans vos décisions, afin d'éviter de vous retrouver **coincer** et de mourir à petit feu.

## Remarques

### Limitations / difficulés rencontrées

- Manque de temps

	> Ce projet nous tient à coeur et nous y avons accordé beaucoup de motivation et de détermination. Nous avons la sensation d'avoir beaucoup appris et sommes fiers de notre rendu. Cependant, nous avions des attentes trop importantes en début de projet par rapport au temps qui nous a été accordé pour réaliser le projet. Nous somme donc un peu déçu du résultat final, car nous avons le potentiel de faire mieux.
     
- Complexité d'Unity

	> Il a été compliqué de prendre en main Unity au début, car le logiciel se base sur de nombreuses notions auquel nous n'avons jamais été confronté auparavant. Cependant, on a vite réussi à prendre en main le logiciel, et nous pensons maintenant maitriser les fondammentaux du développement de jeux sur Unity.
    
- Gestion des collisions : bugs de saut / physique du jeu

	> Lorsque le `Player` se déplaçait, il arrivait qu'il se bloque parfois dans les murs et dans le sol, sans raison particulière. Nous avons du réaliser des scripts afin de détecter certains cas de figure engendrant ce genre de problèmes. Ainsi, suite à cette détection, on fait en sorte de décoller le `Player` de son environnement.

- Détection des layout avec `Raycast`

	> Afin de permettre aux ennemis de se déplacer et de faire facilement demi-tour, nous avons décider d'implémenter un système de `Raycast`. Pour les ennemis, on a ainsi un `Raycast` à l'avant, un à l'arrière et un en contre bas afin de détecter les murs et les sols. Le problème que nous avons rencontré est l'utilisation d'un `LayerMask` afin d'indiquer quel type d'objet détecter. Lorsque nous l'indiquions à la main, nous n'obtenions pas le bon résultat, car le programme n'utilisais pas le masque de bits du layout. Mais en passant le layout via l'inspector, le bon masque de bit était transmis et nous avons pu résoudre notre problème.

- Lancer des Spears

	> Nous avons rencontré des difficultés sur l'implémentation des `Spear`. En effet, il nous fallait réussir à obtenir un lancer avec un mouvement parabolique ciblant le joueur.
Pour le ciblage du joueur, il nous fallait vérifier dans le `OnTriggerStay()`, si je joueur était toujours dans le champs d'attaque de l'ennemi. Ainsi, s'il avait bougé, il fallait actualiser la position de la `Spear`.
Mais la principale difficulté fût de réussir à obtenir un mouvement parabolique. Unity ne nous permettait pas d'obtenir ce résultat, tout en ciblant le joueur. De fait, afin de résoudre ce problème, nous avons utilisé un script open source sur [github](https://gist.github.com/jackchen1210/61fa983c3089dc4b6d58ff44ec47c540).
    
- Relier les animations, les sons et les actions de manière réaliste

	> Afin de gérer les sons de manière simpe et intuitive dans le code source, nous avons réalisé un `AudioManager`. Et nous avons utilisé le système d'`Animator` de Unity pour animer les modèles 3D. Cependant, ces deux systèmes sont difficiles à combiner afin de gérer des actions réalistes. Par exemple, lors de l'attaque du `Player`, le son d'attaque n'est pas joué exactement au bon moment et l'ennemi touché disparaît parfois trop tôt de l'écran.

### Bugs connus

- Problèmes de collision avec les murs

	> Le `Player` reste parfois bloqué dans un mur lors d'un saut.
    
- Ennemis qui se bloquent 

	> Les `AxeMan` et `SpearLauncher` se bloquent parfois dans le sol s'il y a une légère différence de hauteur au niveau des sols.
    
- Problème d'animation sur la grosse attaque du `Player`

	> Lors de l'attaque lourde du `Player`, l'animation est mal gérée. Cette dernière est jouée après le fait que l'attaque soit lancée. En clair, on tue l'ennemi puis on l'attaque.
    
- Bugs quand on spamme les attaques

	> Quand on effectue une grand nombre d'attaques d'affilée avec le `Player`, les sons sont mal gérés. Ces derniers se jouent en même temps, sans attendre que le précédant soit terminé. 

### Voies d'amélioration

- Amélioration du rendu visuel de l'UI ;

- Amélioration des liaisons entre les animations, les sons et les collisions ;

- Ajouter de nouveaux ennemis ;

- Ajouter des boss, que le `Player` devrait affronter ;

- Ajouter des blocs d'environnement (lave, eau, cordes, ...) ;

- Ajouter des nouveaux contrôles au joueur (roulades, sprint, dash, ...) ;

- Gestion des niveaux plus précis, avec un mode "aventure" ;

- Ajout d'un menu de paramètres ;

- Avoir plusieurs sessions de jeu ;

## Song credits

We are **free** to use this music tracks (even for commercial purposes).

### Battle Metal

Battle Metal by Alexander Nakarada | https://www.serpentsoundstudios.com

Music promoted by https://www.chosic.com/

Attribution 4.0 International (CC BY 4.0)
https://creativecommons.org/licenses/by/4.0/

Downloaded on choisic.com : https://www.chosic.com/download-audio/?t=28028.

### Cherry Metal

Cherry Metal by Arthur Vyncke | https://soundcloud.com/arthurvost

Creative Commons Attribution-ShareAlike 3.0 Unported
https://creativecommons.org/licenses/by-sa/3.0/deed.en_US

Music promoted by https://www.chosic.com

Downloaded on choisic.com : https://www.chosic.com/download-audio/?t=27254.

### Maxi Metal & Farming By Moonlight & Ghost Surf Rock
 
Downloaded on choisic.com : 
- https://www.chosic.com/download-audio/?t=25492 ;
- https://www.chosic.com/download-audio/?t=27011 ;
- https://www.chosic.com/download-audio/?t=24604 ;

## Sound credits

We are **free** to use this sound tracks (even for commercial purposes).
We dowloaded this sounds in mixkit.co : https://mixkit.co.