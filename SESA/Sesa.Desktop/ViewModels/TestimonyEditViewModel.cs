using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Sesa.Desktop.Common;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class TestimonyEditViewModel : EditViewModelBase<Testimony>
    {
        public TestimonyEditViewModel()
        {
            MessengerInstance.Register<Guid>(this, Tokens.SelectProductEditTestimony, id =>
                {
                    ProductId = id;
                    SetProduct();
                });
        }

        public Guid ProductId { get; private set; }

        private ObservableCollection<TestimonyDetail> _internals;

        private ObservableCollection<TestimonyDetail> _externals;

        private ObservableCollection<Material> _externalMaterials;

        private ObservableCollection<Material> _internalMaterials;

        public override Tokens Token
        {
            get
            {
                return Tokens.EditTestimony;
            }
        }

        private RelayCommand _calculateCommand;

        /// <summary>
        /// Gets the CalculateCommand.
        /// </summary>
        public RelayCommand CalculateCommand
        {
            get
            {
                return _calculateCommand
                    ?? (_calculateCommand = new RelayCommand(OnCalculate, () => true));
            }
        }

        private RelayCommand _addToInternalMaterialCommand;
        private RelayCommand _addToExternalMaterialCommand;
        private Material _selectedInternalMaterial;
        private Material _selectedExternalMaterial;

        /// <summary>
        /// Gets the AddToInternalMaterialCommand.
        /// </summary>
        public RelayCommand AddToInternalMaterialCommand
        {
            get
            {
                return _addToInternalMaterialCommand
                    ?? (_addToInternalMaterialCommand = new RelayCommand(OnAddToInternalMaterial, () => SelectedExternalMaterial != null));
            }
        }

        private void OnAddToInternalMaterial()
        {
            var temp = SelectedExternalMaterial;
            ExternalMaterials.Remove(SelectedExternalMaterial);
            InternalMaterials.Add(temp);
        }

        /// <summary>
        /// Gets the AddToExternalMaterialCommand.
        /// </summary>
        public RelayCommand AddToExternalMaterialCommand
        {
            get
            {
                return _addToExternalMaterialCommand
                    ?? (_addToExternalMaterialCommand = new RelayCommand(OnAddToExternalMaterial, () => SelectedInternalMaterial != null));
            }
        }

        private void OnAddToExternalMaterial()
        {
            var temp = SelectedInternalMaterial;
            InternalMaterials.Remove(SelectedInternalMaterial);
            ExternalMaterials.Add(temp);
        }

        private void OnCalculate()
        {
            while (Entity.TestimonyDetails.Any())
            {
                TestimonyDetailAccessService.Delete(Entity.TestimonyDetails.First());
            }
            Entity.TestimonyDetails.Clear();
            SetInternalTestimonyDetail();
            SetExternalTestimonyDetail();
            Entity.PureWeight = Entity.TestimonyDetails.Where(p => !p.Material.IsPackingType).Sum(p => p.Weight);
            Entity.RealWeight = Entity.TestimonyDetails.Sum(p => p.Weight);
        }

        private void SetExternalTestimonyDetail()
        {
            foreach (var detail in Externals)
            {
                detail.Testimony = null;
                detail.Material = null;
                detail.WarehouseBill = null;
            }
            Externals.Clear();
            Entity.Product.ExternalProductMaterial.Where(p => ExternalMaterials.Contains(p.Material))
                  .ToList()
                  .ForEach(externalMaterial =>
                      {
                          var currentValue = externalMaterial.Value * Entity.ProductCount;
                          var items = externalMaterial
                              .Material
                              .WarehouseBillDetails
                              .Where(p => !p.WarehouseBill.IsInternal)
                              .Select(p => new
                                  {
                                      WarehouseBill = p.WarehouseBill,
                                      Consume =
                                               p.WarehouseBill.TestimonyDetails.Where(
                                                   q => (q.Testimony == null || q.Testimony.Id != Entity.Id) && q.Material == externalMaterial.Material).Sum(q => q.Value),
                                      Orginal =
                                               p.WarehouseBill.WarehouseBillDetails.Where(
                                                   q => q.Material == externalMaterial.Material).Sum(q => q.Value),
                                  })
                              .Where(p => p.Consume < p.Orginal)
                              .OrderBy(p => p.WarehouseBill.RowNumber).ToArray();
                          var tempValue = currentValue;
                          foreach (var item in items)
                          {
                              var remind = item.Orginal - item.Consume;
                              var testimonyDetail = new TestimonyDetail
                                  {
                                      Id = Guid.NewGuid(),
                                      IsInternal = false,
                                      Material = externalMaterial.Material,
                                      WarehouseBill = item.WarehouseBill,
                                      Value = tempValue > remind ? remind : tempValue,
                                  };
                              Externals.Add(testimonyDetail);
                              Entity.TestimonyDetails.Add(testimonyDetail);
                              tempValue = tempValue - remind;
                              if (tempValue <= 0)
                                  break;
                          }
                          if (tempValue > 0)
                          {
                              var detail = new TestimonyDetail
                                  {
                                      Id = Guid.NewGuid(),
                                      IsInternal = false,
                                      Material = externalMaterial.Material,
                                      Value = tempValue,
                                  };
                              Externals.Add(detail);
                              Entity.TestimonyDetails.Add(detail);
                          }
                      });
        }

        private void SetExternalInInternalTestimonyDetail()
        {
            Entity.Product.ExternalProductMaterial.Where(p => InternalMaterials.Contains(p.Material)).OrderBy(p => p.Sort).ToList().ForEach(externalMaterial =>
            {
                var currentValue = externalMaterial.Value * Entity.ProductCount;
                var items = externalMaterial
                    .Material
                    .WarehouseBillDetails
                    .Where(p => p.WarehouseBill.IsInternal)
                    .Select(p => new
                    {
                        WarehouseBill = p.WarehouseBill,
                        Consume = p.WarehouseBill.TestimonyDetails.Where(q => (q.Testimony == null || q.Testimony.Id != Entity.Id) && q.Material == externalMaterial.Material)
                                  .Sum(q => q.Value),
                        Orginal = p.WarehouseBill.WarehouseBillDetails.Where(q => q.Material == externalMaterial.Material)
                                  .Sum(q => q.Value),
                    })
                    .Where(p => p.Consume < p.Orginal)
                    .OrderBy(p => p.WarehouseBill.RowNumber).ToArray();
                var tempValue = currentValue;
                foreach (var item in items)
                {
                    var remind = item.Orginal - item.Consume;
                    var testimonyDetail = new TestimonyDetail
                        {
                            Id = Guid.NewGuid(),
                            IsInternal = true,
                            Material = externalMaterial.Material,
                            WarehouseBill = item.WarehouseBill,
                            Value = tempValue > remind ? remind : tempValue,
                        };
                    Internals.Add(testimonyDetail);
                    Entity.TestimonyDetails.Add(testimonyDetail);
                    tempValue = tempValue - remind;
                    if (tempValue <= 0)
                        break;
                }
                if (tempValue > 0)
                {
                    var detail = new TestimonyDetail
                        {
                            Id = Guid.NewGuid(),
                            IsInternal = true,
                            Material = externalMaterial.Material,
                            Value = tempValue,
                        };
                    Internals.Add(detail);
                    Entity.TestimonyDetails.Add(detail);
                }
            });
        }

        private void SetInternalTestimonyDetail()
        {
            foreach (var detail in Internals)
            {
                detail.Testimony = null;
                detail.Material = null;
                detail.WarehouseBill = null;
            }
            Internals.Clear();
            Entity.Product.InternalProductMaterials.OrderBy(p => p.Sort).ToList().ForEach(internalMaterial =>
                {
                    var currentValue = internalMaterial.Value * Entity.ProductCount;
                    var items = internalMaterial
                        .Material
                        .WarehouseBillDetails
                        .Where(p => p.WarehouseBill.IsInternal)
                        .Select(p => new
                            {
                                WarehouseBill = p.WarehouseBill,
                                Consume =
                                         p.WarehouseBill.TestimonyDetails.Where(q => (q.Testimony == null || q.Testimony.Id != Entity.Id) && q.Material == internalMaterial.Material)
                                          .Sum(q => q.Value),
                                Orginal =
                                         p.WarehouseBill.WarehouseBillDetails.Where(q => q.Material == internalMaterial.Material)
                                          .Sum(q => q.Value),
                            })
                        .Where(p => p.Consume < p.Orginal)
                        .OrderBy(p => p.WarehouseBill.RowNumber).ToArray();
                    var tempValue = currentValue;
                    foreach (var item in items)
                    {
                        var remind = item.Orginal - item.Consume;
                        var testimonyDetail = new TestimonyDetail
                            {
                                Id = Guid.NewGuid(),
                                IsInternal = true,
                                Material = internalMaterial.Material,
                                WarehouseBill = item.WarehouseBill,
                                Value = tempValue > remind ? remind : tempValue,
                            };
                        Internals.Add(testimonyDetail);
                        Entity.TestimonyDetails.Add(testimonyDetail);
                        tempValue = tempValue - remind;
                        if (tempValue <= 0)
                            break;
                    }
                    if (tempValue > 0)
                    {
                        var detail = new TestimonyDetail
                            {
                                Id = Guid.NewGuid(),
                                IsInternal = true,
                                Material = internalMaterial.Material,
                                Value = tempValue,
                            };
                        Entity.TestimonyDetails.Add(detail);
                        Internals.Add(detail);
                    }
                });
            SetExternalInInternalTestimonyDetail();
        }


        public IDataService<Material> MaterialAccessService { get; private set; }

        public IDataService<Product> ProductAccessService { get; private set; }

        public IDataService<InternalProductMaterial> InternalProductMaterialAccessService { get; private set; }

        public IDataService<TestimonyDetail> TestimonyDetailAccessService { get; private set; }

        public ObservableCollection<TestimonyDetail> Internals
        {
            get { return _internals; }
            set
            {
                _internals = value;
                RaisePropertyChanged(() => Internals);
            }
        }

        public ObservableCollection<TestimonyDetail> Externals
        {
            get { return _externals; }
            set
            {
                _externals = value;
                RaisePropertyChanged(() => Externals);
            }
        }

        public ObservableCollection<Material> InternalMaterials
        {
            get { return _internalMaterials; }
            set
            {
                _internalMaterials = value;
                RaisePropertyChanged(() => InternalMaterials);
            }
        }

        public Material SelectedInternalMaterial
        {
            get { return _selectedInternalMaterial; }
            set
            {
                _selectedInternalMaterial = value;
                AddToInternalMaterialCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => SelectedInternalMaterial);
            }
        }

        public ObservableCollection<Material> ExternalMaterials
        {
            get { return _externalMaterials; }
            set
            {
                _externalMaterials = value;
                RaisePropertyChanged(() => ExternalMaterials);
            }
        }

        public Material SelectedExternalMaterial
        {
            get { return _selectedExternalMaterial; }
            set
            {
                _selectedExternalMaterial = value;
                AddToExternalMaterialCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => SelectedExternalMaterial);
            }
        }

        protected override void LoadData(string key)
        {
            MaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<Material>>(key);
            MaterialAccessService.SyncContext(key);
            if (Mode == FormMode.Edit)
                InternalMaterials = new ObservableCollection<Material>(Entity.Product.ExternalProductMaterial.Select(p => p.Material).Except(ExternalMaterials));
            else
                InternalMaterials = new ObservableCollection<Material>();
            TestimonyDetailAccessService = SimpleIoc.Default.GetInstance<IDataService<TestimonyDetail>>(key);
            TestimonyDetailAccessService.SyncContext(key);

            Func<TestimonyDetail, int> func = p =>
                {
                    var inpm = p.Testimony.Product.InternalProductMaterials.FirstOrDefault(q => p.Material == q.Material);
                    if (inpm != null)
                        return inpm.Sort;
                    var expm = p.Testimony.Product.ExternalProductMaterial.FirstOrDefault(q => p.Material == q.Material);
                    if (expm != null)
                        return expm.Sort;
                    return 0;
                };
            Internals = new ObservableCollection<TestimonyDetail>(Entity.TestimonyDetails.Where(p => p.IsInternal).OrderBy(func));
            Externals = new ObservableCollection<TestimonyDetail>(Entity.TestimonyDetails.Where(p => !p.IsInternal).OrderBy(func));

            ProductAccessService = SimpleIoc.Default.GetInstance<IDataService<Product>>(key);
            ProductAccessService.SyncContext(key);

            InternalProductMaterialAccessService = SimpleIoc.Default.GetInstance<IDataService<InternalProductMaterial>>(key);
            InternalProductMaterialAccessService.SyncContext(key);
        }
        protected override void OnEntityChanged()
        {
            base.OnEntityChanged();
            if (Entity != null && Mode == FormMode.Edit)
                ExternalMaterials = new ObservableCollection<Material>(
                    Entity.Product.ExternalProductMaterial.Select(p => p.Material)
                    .Except(Entity.TestimonyDetails.Where(p => p.IsInternal).Select(q => q.Material)));
        }
        private void SetProduct()
        {
            if (Entity != null)
            {
                if (Entity.Product == null && ProductId != Guid.Empty)
                {
                    Entity.Product = ProductAccessService.GetByID(ProductId);
                    if (Entity.Product != null)
                        Entity.PackingType = Entity.Product.Caption;
                    ExternalMaterials = new ObservableCollection<Material>(Entity.Product.ExternalProductMaterial.Select(p => p.Material));
                }
            }
        }

        protected override bool OnSave()
        {
            //            if (Mode == FormMode.Edit)
            //            {
            //                MessageBoxHelper.Show("مجاز به ویرایش گواهی تولید نیستید");
            //                return false;
            //            }

            if (Internals.Union(Externals).Any(p => p.WarehouseBill == null))
            {
                var error = string.Join(" و ", Internals.Union(Externals).Where(p => p.WarehouseBill == null).Select(p => string.Format("\"{0}\"", p.Material.Caption)));
                MessageBoxHelper.Show(string.Format("محصولات {0} در انبار موجود نیستند", error));
                return false;
            }
            UpdateDetails();

            if (base.OnSave())
            {
                var navigation = SimpleIoc.Default.GetInstance<INavigation>("TestimonyReportView");
                MessengerInstance.Send(Entity.HeaderNumber, Tokens.TestimonyReport);
                NavigationManagert.NavigateTo(navigation);
                return true;
            }
            return false;
        }

        private void UpdateDetails()
        {
            Internals.ToList().ForEach(p => p.IsInternal = true);
            Externals.ToList().ForEach(p => p.IsInternal = false);

            var details = Internals.Union(Externals).ToArray();
            var addeds = details.Where(p => p.Id == Guid.Empty).ToArray();
            var deleteds = Entity.TestimonyDetails.Except(details).ToArray();

            foreach (var addedObj in addeds)
            {
                Entity.TestimonyDetails.Add(addedObj);
            }

            foreach (var deleteObj in deleteds)
            {
                TestimonyDetailAccessService.Delete(deleteObj);
            }
        }
    }
}