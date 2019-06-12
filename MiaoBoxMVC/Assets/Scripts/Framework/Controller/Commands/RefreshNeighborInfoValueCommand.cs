using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

class RefreshNeighborInfoValueCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        NeighborInfoProxy neighborInfoProxy = (NeighborInfoProxy)Facade.RetrieveProxy(NeighborInfoProxy.NAME);
        neighborInfoProxy.RefreshNeighborInfoValue();
    }
}