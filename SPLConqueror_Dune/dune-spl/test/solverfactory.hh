#ifndef DUNE_SPL_SOLVERFACTORY_HH
#define DUNE_SPL_SOLVERFACTORY_HH

#include <dune/pdelab/stationary/linearproblem.hh>
#include <dune/pdelab/newton/newton.hh>

namespace Dune
{
  namespace SPL
  {
    template <class Solver>
    struct SolverFactory;

    template <class GO, class LS, class U>
    struct SolverFactory<Dune::PDELab::StationaryLinearProblemSolver<GO, LS, U> >
    {
      using Type = Dune::PDELab::StationaryLinearProblemSolver<GO, LS, U>;
      static Type construct(GO& go, LS& ls, U& u, double reduction)
      {
        return Type(go, ls, u, reduction);
      }
    };

    template <class GO, class LS, class U, class V>
    struct SolverFactory<Dune::PDELab::Newton<GO, LS, U, V> >
    {
      using Type = Dune::PDELab::Newton<GO, LS, U, V>;
      static Type construct(GO& go, LS& ls, U& u, double reduction)
      {
        Type newton(go, u, ls);
        newton.setReduction(reduction);
        return newton;
      }
    };
  }
}

#endif // DUNE_SPL_SOLVERFACTORY_HH
