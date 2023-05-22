using System;
using BaseInfrastructure;
using UniRx;

namespace MainComponents.Gifts.TrashBin
{
    public interface ITrashBinView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
    }
}