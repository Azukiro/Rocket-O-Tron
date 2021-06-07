# Release Notes

Documentation technique de la **version 1.0.4** de **Dark Mario**.

|                      |Données         |Commentaire                                         |
|----------------------|----------------|----------------------------------------------------|
|Nom du projet         |`"Dark Mario"`  |Inspiré de Dark Souls et de Super Mario             |
|Date de diffusion     |25 juin 2021    |Date de rendu du projet                             |
|Date de la release    |2 juin 2021     |                                                    |
|Version de la release |Version 1.0.4   |                                                    |
|Auteur de la release  |Fabien COURTOIS |Gameplay Developer / Sound Designer                 | 
|Auteur de la release  |Lucas  BILLARD  |Gameplay Developer / Game/Level Designer            | |Auteur de la release  |Ewen BOUQUET    |Producer & Graphiste 2D/3D & Music & Sound Designer |

## Description générale

Déclenchement des **animations 3D** du Player en fonction des actions déterminées par l'utilisateur.
Gestion des collisions entre le Player et les murs.

## Problèmes

Lorsque le Player sautait vers un mur, son épée se plantait parfois dans le mur, ce qui bloquait le Player dans le mur. Nous avons donc du trouver une solution à ce problème, car ce dernier bloquait complètement le gameplay du jeu.

## Nouveautés

Ce premier lot de développements inclut la réalisation des tâches suivantes.
- Actualisation du script Player afin de déclencher les animations au bon moment dans le `.Update()` ;
	>  `Idle` : Regarde autour de lui et bouge son épée si aucune action n'est réalisée ;
	>  `Move` : Déplacement horizontal ;
	>  `Jump` : Déplacement vertical ;
	>  `Attack` : Met un coup d'épée devant lui ;
	>  `BigAttack` : Met un grand coup d'épée tranchant ;
	>  `Block` : Se protège derrière son bouclier ;

- Attribution de colliders aux objets 3D Player, AxeEnemy et aux murs ;
- Rédaction d'un script pour décoller le Player du mur quand il y a un problème ;

## Plus values

### Côté développeur

Pas de plus value.

### Côté utilisateur

Le jeu est beaucoup plus vivant et le Player ne peut plus se bloquer dans les murs

