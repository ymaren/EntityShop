using System.Collections.Generic;

namespace Store.Logic.ProductStore.Infustructure
{
    public interface IViewService<TViewModel>
    {
        TViewModel ViewSingle(int id);

        IEnumerable<TViewModel> ViewAll();
    }
}
