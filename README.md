# Kata : SouleRoyale
La Soule Royales est un jeu où s'affronte deux équipes de 11 joueurs. La première équipe à mettre la soule dans la zone d'embute adverse remporte la partie.

## Objectif
L'objectif de ce kata est de coder l'arbitre. C'est à le moteur permettant de déterminer l'évolution de la partie. Il conviendra donc : 
- lire les instructions fournies par les capitaines des deux équipes
- afficher la position de la soule, des joueurs et de leur état découlant des instructions
   
# Déroulement d'un match
Le terrain est composé de 7 lignes. Les 2 extrémités sont les zones d'embutes. Chaque joueur à 4 points de vie.
Lors de la phase initiale, la soule est au centre du terrain. Les capitaines donnent à l'arbitre la position de leurs joueurs sur le terrain.
Chaque case est représentée par un nombre allant de 1, la ligne la plus proche de la zone centrale, à 3 la zone d'embute de l'équipe.

0. Phase initiales placement des joueurs sur le terrain
1. Instructions des capitaines
2. Résolution des inscrutctions en commençant par le joueur n°1 de l'équipe active, puis le n°1 de l'équipe passive, puis le n°2 de l'équipe active et ainsi de suite...
   __Dès que la soule se trouve en zone d'embute, la partie s'arrète.__
3. Fin de la phase de résolution, l'autre équipe devient l'équipe active et on recommence au point 1

_Ex :_
- $\color{red}{Equipe\ 1: 2\ 1\ 1\ 1\ 1\ 2\ 1\ 1\ 1\ 1\ 2}$
- $\color{blue}{Equipe\ 2: 1\ 1\ 1\ 1\ 2\ 1\ 1\ 2\ 2\ 2\ 1}$

  ![image](https://github.com/user-attachments/assets/34ebde7f-8448-4d06-87f2-03ff7d1c2f91)


Ensuite, une fois les joueurs sur le terrain, les 2 capitianes fournissent à l'arbitre leurs instructions.
Les actions possibles sont :
- ne rien faire : `0`
- avancer d'une ligne : `+`
  
  _Lorsqu'un joueur se déplace, si la soule se trouve sur la ligne suivante, automatiquement, celle-ci avance d'une ligne également._
  ![Alt Text](http://soule.royale.free.fr/Images/regles/Pousse_soule.gif)
- reculer d'une ligne : `-`
- frapper le joueur n° X où X est le numéro du joueur : `X`

_Ex :_
- $\color{red}{Equipe\ 1: 0\ -\ 0\ 0\ 0\ +\ +\ +\ +\ +\ -}$
- $\color{blue}{Equipe\ 2: +\ +\ +\ +\ 0\ +\ +\ 0\ +\ +\ +\}$

Pour résoudre les insctuctions, l'arbitre commence donc par l'action du joueur $\color{red}{n°1}$ de l'équipe active. Son capitaine a choisi qu'il ne fasse rien.
Ensuite l'arbitre applique l'action du joueur $\color{blue}{n°1}$ de l'équipe passive. C'est un `+`. Il avance d'une ligne et se retrouve donc en ligne centrale. La $\color{orange}{soule}$ se trouve sur cette ligne. Elle avance donc également d'une ligne.

_Ci-dessous l'exemple animé de la résolution à partir des instructions de l'exemple précédent._

  ![Alt Text](http://soule.royale.free.fr/Images/regles/Phase1.gif)


Le tour est terminé.  $\color{blue}{L'équipe 2} devient l'équipe active.


