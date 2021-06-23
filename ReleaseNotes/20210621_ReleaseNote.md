# Release Notes

Documentation technique de la **version 1.0.11** de **Dark Mario**.

|                      |Données         |Commentaire                                         |
|----------------------|----------------|----------------------------------------------------|
|Nom du projet         |`"Dark Mario"`  |Inspiré de Dark Souls et de Super Mario             |
|Date de diffusion     |23 juin 2021    |Date de rendu du projet                             |
|Date de la release    |21 juin 2021    |                                                    |
|Version de la release |Version 1.0.11  |                                                    |
|Auteur de la release  |Ewen BOUQUET    |Producer & Graphiste 2D/3D & Music & Sound Designer |  

## Description générale

Gestion de l'ui : 
- Pause et Resume ; 
- Changement de niveaux ;
- Conditions de victoire et de défaite ;

## Nouveautés

Ce lot de développements inclut la réalisation des tâches suivantes :
- Pause et Resume ;

	  > Utilisation de `Time`.`timeScale` pour mettre le jeu en pause en fond, et pouvoir le reprendre ;

      > Blocage de l'ensemble des actions et des mécaniques de jeu ;

- Changement de niveaux ;

      > Rechargement de la `Scene` courante lors du `Play` ;

      > Chargement de niveaux plus élevés via le bouton `Next level` ;

- Conditions de victoire et de défaite ;

      > Victoire lors de l'arrivée du `Player` dans la `FinalDoor` ;

      > Défaite lors de la chute du nombre de vies du `Players` ou lors d'un temps de réalisation de niveau trop long ;

## Plus values

### Côté développeur

- Possibilité de créer les différents niveaux pour la version finale ;
- Code générique et facilement adaptable grace au pattern  `Observer` ;

### Côté utilisateur

Le joueur peut vraiment jouer au jeu dans sa globalité, et peut choisir son niveau.

