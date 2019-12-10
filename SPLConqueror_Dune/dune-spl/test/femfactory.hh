#ifndef DUNE_SPL_FEMFACTORY_HH
#define DUNE_SPL_FEMFACTORY_HH

#include <dune/pdelab/finiteelementmap/qkfem.hh>
#include <dune/pdelab/finiteelementmap/pkfem.hh>
#include <dune/pdelab/finiteelementmap/qkdg.hh>
#include <dune/pdelab/finiteelementmap/opbfem.hh>

namespace Dune
{
  namespace SPL
  {
    template <class FEM>
    struct FEMFactory;

    template <class GV, class DF, class RF, std::size_t deg>
    struct FEMFactory<Dune::PDELab::QkLocalFiniteElementMap<GV, DF, RF, deg> >
    {
      using Type = Dune::PDELab::QkLocalFiniteElementMap<GV, DF, RF, deg>;
      static Type construct(const GV& gv) { return Type(gv); }
    };

    template <class GV, class DF, class RF, unsigned int deg>
    struct FEMFactory<Dune::PDELab::PkLocalFiniteElementMap<GV, DF, RF, deg> >
    {
      using Type = Dune::PDELab::PkLocalFiniteElementMap<GV, DF, RF, deg>;
      static Type construct(const GV& gv) { return Type(gv); }
    };

    template <class D, class R, int k, int d>
    struct FEMFactory<Dune::PDELab::QkDGLocalFiniteElementMap<D, R, k, d> >
    {
      using Type = Dune::PDELab::QkDGLocalFiniteElementMap<D, R, k, d>;
      template <class GV>
      static Type construct(const GV& gv)
      {
        return Type();
      }
    };

    template <class D, class R, int k, int d, Dune::GeometryType::BasicType bt,
              typename ComputationFieldType, PB::BasisType basisType>
    struct FEMFactory<Dune::PDELab::OPBLocalFiniteElementMap<D, R, k, d, bt, ComputationFieldType,
                                                             basisType> >
    {
      using Type
          = Dune::PDELab::OPBLocalFiniteElementMap<D, R, k, d, bt, ComputationFieldType, basisType>;
      template <class GV>
      static Type construct(const GV& gv)
      {
        return Type();
      }
    };
  }
}

#endif // DUNE_SPL_FEMFACTORY_HH
