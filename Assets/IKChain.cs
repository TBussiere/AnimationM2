using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class IKChain
    {
        // Quand la chaine comporte une cible pour la racine. 
        // Ce sera le cas que pour la chaine comportant le root de l'arbre.
        private IKJoint rootTarget = null;

        // Quand la chaine à une cible à atteindre, 
        // ce ne sera pas forcément le cas pour toutes les chaines.
        //private IKJoint endTarget = null;

        //private IKJoint endTarget = null;

        private Transform endTarget = null;

        // Toutes articulations (IKJoint) triées de la racine vers la feuille. N articulations.
        private List<IKJoint> joints = new List<IKJoint>();

        // Contraintes pour chaque articulation : la longueur (à modifier pour 
        // ajouter des contraintes sur les angles). N-1 contraintes.
        private List<float> constraints = new List<float>();


        //private static List<IKJoint> jointExporedList = new List<IKJoint>();


        // Un cylndre entre chaque articulation (Joint). N-1 cylindres.
        //private List<GameObject> cylinders = new List<GameObject>();    



        // Créer la chaine d'IK en partant du noeud endNode et en remontant jusqu'au noeud plus haut, ou jusqu'à la racine
        public IKChain(Transform _endNode, Transform _rootTarget = null, Transform _endTarget = null)
        {
            Debug.Log("=== IKChain::createChain: ===");
            // TODO : construire la chaine allant de _endNode vers _rootTarget en remontant dans l'arbre. 
            // Chaque Transform dans Unity a accès à son parent 'tr.parent'

            while (_endNode.name != _rootTarget.name)
            {
                IKJoint addedJoint = new IKJoint(_endNode);
                joints.Add(addedJoint);

                _endNode = _endNode.parent;
            }

            rootTarget = new IKJoint(_rootTarget);
            //endTarget = new IKJoint(_endTarget);
            endTarget = _endTarget;
        }


        public void Merge(IKJoint j)
        {
            if (First().name == j.name)
            {
                joints[0] = j;
            }
            if (Last().name == j.name)
            {
                joints[joints.Count - 1] = j;
            }
        }


        public IKJoint First()
        {
            return joints[0];
        }
        public IKJoint Last()
        {
            return joints[joints.Count - 1];
        }

        public void Backward()
        {
            //Debug.Log(" 1 : " + joints[1].name + " 2 : " + First().name);
            float nextLength = (joints[1].position - First().position).magnitude;
            First().SetPosition(endTarget.position);

            IKJoint oldn = First();
            
            for (int i = 1; i < joints.Count; i++)
            {
                float length = nextLength;
                if (i + 1 < joints.Count)
                {
                    nextLength = (joints[i + 1].position - joints[i].position).magnitude;
                }
                joints[i].Solve(oldn, length);

                oldn = joints[i];
                
            }
        }

        public void Forward()
        {
            // TODO : une passe descendante de FABRIK. Placer le noeud 0 sur son origine puis on descend.
            // Codez et deboguez déjà Backward avant d'écrire celle-ci.

            float nextLength = (joints[joints.Count-2].position - Last().position).magnitude;
            Last().SetPosition(rootTarget.position);
            IKJoint oldn = Last();

            for (int i = joints.Count - 2; i >= 0; i--)
            {
                float length = nextLength;
                if (i - 1 > 0)
                {
                    nextLength = (joints[i - 1].position - joints[i].position).magnitude;
                }
                joints[i].Solve(oldn, length);

                oldn = joints[i];

            }
        }

        public void ToTransform()
        {
            // TODO : pour tous les noeuds de la liste appliquer la position au transform : voir ToTransform de IKJoint
            for (int i = joints.Count-1; i >= 0; i--)
            {
                joints[i].ToTransform();
            }
        }

        public void Check()
        {
            // TODO : des Debug.Log pour afficher le contenu de la chaine (ne sert que pour le debug)
            Debug.Log(joints.Count);

            this.joints.ForEach(j => Debug.Log("je suis "+ j.name + " pos:"+j.position));

            Debug.Log("end target = " + endTarget.position);

            Debug.Log("root target = " + rootTarget.position);
        }
    }
}
