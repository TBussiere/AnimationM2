using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IK : MonoBehaviour
{
    // Le transform (noeud) racine de l'arbre, 
    // le constructeur créera une sphère sur ce point pour en garder une copie visuelle.
    public List<GameObject> rootNode = null;

    // Un transform (noeud) (probablement une feuille) qui devra arriver sur targetNode
    public List<Transform> srcNode = null;

    // Le transform (noeud) cible pour srcNode
    public List<Transform> targetNode = null;

    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;

    // Toutes les chaines cinématiques 
    public List<IKChain> chains = new List<IKChain>();

    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 1;


    void Start()
    {
        if (createChains)
        {
            Debug.Log("(Re)Create CHAIN");

            // TODO : 
            // Création des chaines : une chaine cinématique est un chemin entre deux nœuds carrefours.
            // Dans la 1ere question, une unique chaine sera suffisante entre srcNode et rootNode.

            if (srcNode.Count != rootNode.Count || targetNode.Count != srcNode.Count)
            {
                Debug.LogError("Les tailles de tableau ne sont pas les meme");
                return;
            }


            for (int i = 0; i < srcNode.Count; i++)
            {
                if (targetNode[i].name == "NULL")
                {
                    chains.Add(new IKChain(srcNode[i], rootNode[i].transform, null));
                }
                else
                {
                    chains.Add(new IKChain(srcNode[i], rootNode[i].transform, targetNode[i]));
                }   
            }


            // TODO-2 : Pour parcourir tous les transform d'un arbre d'Unity vous pouvez faire une fonction récursive
            // ou utiliser GetComponentInChildren comme ceci :
            // foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())


            // TODO-2 : Dans le cas où il y a plusieurs chaines, fusionne les IKJoint entre chaque articulation.
            for (int i = 0; i < chains.Count; i++)
            {
                for (int j = 0; j < chains.Count; j++)
                {
                    chains[i].Merge(chains[j].First());
                    chains[i].Merge(chains[j].Last());
                }
            }

            createChains = false;
        }
    }

    void Update()
    {
        if (createChains)
            Start();

        //if (Input.GetKeyDown(KeyCode.I))
        //{
            IKOneStep(true);
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Chains count=" + chains.Count);
            foreach (IKChain ch in chains)
                ch.Check();
        }
    }


    void IKOneStep(bool down)
    {
        int j;

        for (j = 0; j < nb_ite; ++j)
        {
            // TODO : IK Backward (remontée), appeler la fonction Backward de IKChain 
            // sur toutes les chaines cinématiques.

            chains.ForEach((ik) => ik.Backward());

            // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain

            chains.ForEach((ik) => ik.ToTransform());

            // IK Forward (descente), appeler la fonction Forward de IKChain 
            // sur toutes les chaines cinématiques.

            chains.ForEach((ik) => ik.Forward());

            // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain

            chains.ForEach((ik) => ik.ToTransform());
        }



    }


}