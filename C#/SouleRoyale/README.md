# Kata Soule Royale C#
Pour ce kata j'ai souhaité utiliser la lib 🥒Gherkin🥒 [Gherkin.Quick](https://github.com/ttutisani/Xunit.Gherkin.Quick) de [Tutisani](https://www.tutisani.com/bdd/).
- [Tutoriel](https://youtu.be/RBcJYt2g_gE)

Mon objectif étant principalement d'éprouver Gherkin, dans les conditions d'un kata, en le manipulant, moi-même, sans contraintes, pour me faire un avis sur l'implémentation qui en découle.
Je m'intéresse à Gherkin car je trouve le cadre posé par Gherkin excellent pour formaliser avec le métier les cas d'usages réels et si en plus on peut s'auppuyer dessus pour faire une documentation vivante, ça me semble valoir le détour.
Je n'ai pas encore éprouvé la partie génération de documentation. 

## Rex sur la lib Gherkin.Quick
😍 J'ai trouvé la prise en main rapide et simple. 

😍 Il y a tout ce dont j'avais besoin pour mon kata.

😩 Le principal défaut de cette lib étant l'équipe de developpement limité à Tutisani et lui seul. Quid de sa périnité ?

😔 Elle n'existe qu'en anglais. Ce n'est pas bien gênant mais dans certain cas de figure cela pourrait-être rédhibitoire.

## Rex Gherkin

😩 J'ai trouvé l'implémentation des scenarii verbeuse et lourde. Rapidement, je me suis lassé. De fait j'ai eu tendance à faire des scénarii plus haut niveau engloblant plusieurs fonctionnalités ce qui est un anti pattern . 

😔 Pour coller au pattern : une étape de scénario Gherkin - une fonction, j'ai été contraint de démultiplier les petites fonctions avec de 1 à 3 lignes de code.

😔 Dans les cas aux limites levant des exceptions, j'ai été contraint : 
- soit de faire des fonctions vides 
  https://github.com/antrax2013/SouleRoyale/blob/215afdfed3297ff1efb151948565793e489f0e1d/C%23/SouleRoyale/SouleRoyale.Tests/TestCases/Player.Tests.cs#L48
- soit d'utiliser des variables privées de la classe de test pour partager des états entre les étapes
  https://github.com/antrax2013/SouleRoyale/blob/215afdfed3297ff1efb151948565793e489f0e1d/C%23/SouleRoyale/SouleRoyale.Tests/TestCases/Move/MirrorMove.Test.cs#L15
  https://github.com/antrax2013/SouleRoyale/blob/215afdfed3297ff1efb151948565793e489f0e1d/C%23/SouleRoyale/SouleRoyale.Tests/TestCases/Move/MirrorMove.Test.cs#L28
Dans les 2 cas je ne trouve pas cela génial. 🧐 A voir pour trouver une moyen de contournement.
> J'aurais pu faire autrement et utiliser des `Result` mais c'est un parti pris que je n'ai retenu

En conséquence, je me pose la question de la pérénité et de la maintenabilité à long terme de Gherkin. 
Je l'utiliserais plutôt pour des tests plus haut niveau que pour du test unitaire dans le cadre du TDD.

