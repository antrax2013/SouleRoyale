# Kata : La Soule Royale
La Soule Royale est un jeu où s'affronte deux équipes de 11 joueurs. La première équipe à mettre la soule dans la zone d'embut adverse remporte la partie.

## Objectif
L'objectif de ce kata est de coder l'arbitre. C'est à dire le moteur permettant de déterminer l'évolution de la partie. Il conviendra donc de : 
- lire les instructions fournies par les capitaines des deux équipes
- résoudre les inscrutctions
- afficher à la fin de chaque tour la position de la soule, des joueurs ainsi que le nombre de point de vie découlant des instructions

# Déroulement d'un match
Le terrain est composé de 7 lignes. Les 2 extrémités sont les zones d'embuts. Chaque joueur à 4 points de vie.
Lors de la phase initiale, la soule est au centre du terrain. Les capitaines donnent à l'arbitre la position de leurs joueurs sur le terrain.
Chaque case est représentée par un nombre allant de 1, la ligne la plus proche de la zone centrale, à 3 la zone d'embut de l'équipe.

0. Phase initiales placement des joueurs sur le terrain
1. Instructions des capitaines
2. Résolution des inscrutctions en commençant par le joueur n°1 de l'équipe active, puis le n°1 de l'équipe passive, puis le n°2 de l'équipe active et ainsi de suite...
   __Dès que la soule se trouve en zone d'embut, la partie s'arrète.__
3. Fin de la phase de résolution, l'autre équipe devient l'équipe active et on recommence au point 1

## Phase Initiale
_Instructions :_
- $\color{red}{Equipe\ 1: 2\ 1\ 1\ 1\ 1\ 2\ 1\ 1\ 1\ 1\ 2\}$
- $\color{blue}{Equipe\ 2: 1\ 1\ 1\ 1\ 2\ 1\ 1\ 2\ 2\ 2\ 1\}$

  ![image](https://github.com/user-attachments/assets/34ebde7f-8448-4d06-87f2-03ff7d1c2f91)

## Les instructions
Ensuite, une fois les joueurs sur le terrain, les 2 capitianes fournissent à l'arbitre leurs instructions.
Les actions possibles sont :
- ne rien faire : `0`
- avancer d'une ligne : `+`
  
  _Lorsqu'un joueur se déplace, si la soule se trouve sur la ligne suivante, le joueur, obligatoirement, la pousse et donc celle-ci avance également d'une ligne._
  
  ![Alt Text](http://soule.royale.free.fr/Images/regles/Pousse_soule.gif)
- reculer d'une ligne : `-`
  _Reculer ne fait pas bouger la soule_
- frapper le joueur n° A où A est le numéro du joueur qui est ciblé : `A`
  _Lors de la résolution de l'action du joueur en courant, il ne parviendra à frapper son adversaire uniquement si celui-ci se trouve sur la même ligne que lui au moment où l'arbitre résout son action. Si tel est le cas, le joueur subissant le coup perd 1 point de vie. Un joueur sans point de vie est Ko._
- `X` : un joueur Ko

## Tour 1
_Instructions :_
- $\color{red}{Equipe\ 1: 0\ -\ 0\ 0\ 0\ +\ +\ +\ +\ +\ -\}$ _équipe active_
- $\color{blue}{Equipe\ 2: +\ +\ +\ +\ 0\ +\ +\ 0\ +\ +\ +\}$

Pour résoudre les insctuctions, l'arbitre commence donc par l'action du joueur $\color{red}{n°1}$ de l'équipe active. Son capitaine a choisi qu'il ne fasse rien.
Ensuite l'arbitre applique l'action du joueur $\color{blue}{n°1}$ de l'équipe passive. C'est un `+`. Il avance d'une ligne et se retrouve donc en ligne centrale. La $\color{orange}{soule}$ se trouve sur cette ligne. Elle avance donc également d'une ligne.

_Ci-dessous l'exemple animé de la résolution à partir des instructions de l'exemple précédent._

  ![Alt Text](http://soule.royale.free.fr/Images/regles/Phase1.gif)


Le tour est terminé. L' $\color{blue}{équipe\ 2}$ devient l'équipe active.

## Tour 2
_Instructions :_
- $\color{red}{Equipe\ 1: 0\ +\ 1\ +\ 1\ +\ +\ 11\ 11\ +\ +\}$
- $\color{blue}{Equipe\ 2: +\ 7\ 7\ +\ +\ 7\ 7\ +\ +\ +\ 8\}$ _équipe active_

![Alt Text](http://soule.royale.free.fr/Images/regles/Phase2.gif)
> _Légende :_
> - Chaque point noir, représente une blessure, à 4 c'est le KO

## Tour 3
- $\color{red}{Equipe 1 : +\ +\ 1\ +\ 1\ 11\ X\ 11\ 11\ +\ 0\}$ _équipe active_
- $\color{blue}{Equipe 2 : -\ 9\ 9\ -\ -\ 9\ 9\ 8\ 8\ 8\ 8\}$

![Alt Text](http://soule.royale.free.fr/Images/regles/Phase3.gif)

__La victoire revient à l'équipe rouge.__


# Le Kata
Pour les tests vous pouvez utiliser les instructions ci-dessous

- Phase initiale 
  
  Equipe 1 : 2 1 1 1 1 2 1 1 1 1 2
  
  Equipe 2 : 1 1 1 1 2 1 1 2 2 2 1

- Tour 1 : 
  
  Equipe 1 : 0 - 0 0 0 + + + + + -
  
  Equipe 2 : + + + + 0 + + 0 + + +
  
- Tour 2 :
  
  Equipe 1 : 0 + 1 + 1 + + 11 11 + +
  
  Equipe 2 : + 7 7 + + 7 7 + + + 8
  
- Tour 3:
  
  Equipe 1 : + + 1 + 1 11 X 11 11 + 0
  
  Equipe 2 : - 9 9 - - 9 9 8 8 8 8 

Vainqueur Equipe 1 au tour 3

# Auteur
[![build](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/cyril-cophignon-b58b5a5b/)



