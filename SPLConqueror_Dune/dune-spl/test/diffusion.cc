// -*- tab-width: 4; indent-tabs-mode: nil -*-
/** \file
    \brief High-level test with Poisson equation
*/
#ifdef HAVE_CONFIG_H
#include "config.h"
#endif
#include <iostream>
#include <vector>
#include <map>
#include <dune/common/gmpfield.hh>
#include <dune/common/parallel/mpihelper.hh>
#include <dune/common/exceptions.hh>
#include <dune/common/fvector.hh>
#include <dune/common/static_assert.hh>
#include <dune/common/timer.hh>
#include <dune/grid/yaspgrid.hh>
#include <dune/istl/bvector.hh>
#include <dune/istl/operators.hh>
#include <dune/istl/solvers.hh>
#include <dune/istl/preconditioners.hh>
#include <dune/istl/io.hh>
#include <dune/istl/paamg/amg.hh>
#include <dune/istl/superlu.hh>
#include <dune/grid/yaspgrid.hh>
#if HAVE_UG
#include <dune/grid/uggrid.hh>
#endif
#if HAVE_DUNE_ALUGRID
#include <dune/alugrid/grid.hh>
#endif
#include <dune/grid/io/file/vtk/subsamplingvtkwriter.hh>
#include <dune/grid/utility/structuredgridfactory.hh>
#include <dune/pdelab/backend/istlmatrixbackend.hh>
#include <dune/pdelab/backend/istlsolverbackend.hh>
#include <dune/pdelab/backend/istlvectorbackend.hh>
#include <dune/pdelab/boilerplate/pdelab.hh>
#include <dune/pdelab/common/function.hh>
#include <dune/pdelab/common/functionutilities.hh>
#include <dune/pdelab/common/vtkexport.hh>
#include <dune/pdelab/constraints/common/constraints.hh>
#include <dune/pdelab/constraints/conforming.hh>
#include <dune/pdelab/finiteelementmap/monomfem.hh>
#include <dune/pdelab/finiteelementmap/opbfem.hh>
#include <dune/pdelab/finiteelementmap/pkfem.hh>
#include <dune/pdelab/finiteelementmap/qkdg.hh>
#include <dune/pdelab/finiteelementmap/qkfem.hh>
#include <dune/pdelab/finiteelementmap/rannacherturekfem.hh>
#include <dune/pdelab/gridfunctionspace/gridfunctionspace.hh>
#include <dune/pdelab/gridfunctionspace/gridfunctionspaceutilities.hh>
#include <dune/pdelab/gridfunctionspace/interpolate.hh>
#include <dune/pdelab/gridoperator/gridoperator.hh>
#include <dune/pdelab/localoperator/convectiondiffusiondg.hh>
#include <dune/pdelab/localoperator/convectiondiffusionfem.hh>
#include <dune/pdelab/localoperator/convectiondiffusionparameter.hh>
#include <dune/pdelab/stationary/linearproblem.hh>

#include <dune/pdelab/gridfunctionspace/vtk.hh>

#include "problemA.hh"
#include "femfactory.hh"
#include "solverfactory.hh"

int main(int argc, char** argv)
{
  try {
    // Maybe initialize Mpi
    Dune::MPIHelper& helper = Dune::MPIHelper::instance(argc, argv);

    // ##### variation point #####
    // ### domain dimension
    //const int dim = 1;
    const int dim = 2;
    //const int dim = 3;

    // coordinate and result type
    using Real = double;

    // ##### variation point #####
    // ### select type of grid elements
    
    //VARIATIONPOINT(BET; const Dune::GeometryType::BasicType basicElementType; Dune::GeometryType::cube;)
    const Dune::GeometryType::BasicType basicElementType = Dune::GeometryType::cube;
    //const Dune::GeometryType::BasicType basicElementType = Dune::GeometryType::simplex;	
    
    // ##### variation point #####
    // ### construct grid. note that some grids only work for certain dimensions (eg OneDGrid only
    // ### works for dim==1), while other grids only work for certain element types (eg YaspGrid only
    // ### works with cubes)

    
    //VARIATIONPOINT(GRID; using Grid; Dune::GridDefaultImplementation<dim,dim,typename Coordinates::ctype,YaspGridFamily<dim>>:;)
    using Grid = Dune::GridDefaultImplementation<dim,dim,typename Coordinates::ctype,YaspGridFamily<dim>>:;
    //using Grid = Dune::YaspGrid<dim>; // according to the xml File, there have to be coordinates here. As a consequence, we need two parameters.
    //using Grid = Dune::OneDGrid;
    //using Grid = Dune::ALUGrid < dim, dim, basicElementType == Dune::GeometryType::cube ? Dune::cube : Dune::simplex, Dune::conforming > ;
    //using Grid = Dune::ALUGrid < dim, dim, basicElementType == Dune::GeometryType::cube ? Dune::cube : Dune::simplex, Dune::nonconforming > ;
    //using Grid = Dune::UGGrid<dim>;

    // use a helper class to construct unit-cube domain
    const unsigned int cells = 32;
    Dune::PDELab::StructuredGrid<Grid> grid(basicElementType, cells);
    using GV = Grid::LeafGridView;

    // retrieve grid view
    const GV& gv = grid->leafGridView();

    // construct model problem
    using Problem = Dune::SPL::ProblemA<GV, Real>;
    Problem problem;

    // ##### variation point #####
    // ### set order of shape functions
    const int degree = 1;
    //const int degree = 2;
    //const int degree = 3;

    // ##### variation point #####
    // ### construct finite element map. The degree is a template parameter of the finite element
    // ### map. Note that some FEMs only work for certain element types (eg QkLocalFiniteElementMap only
    // ### works for cubes, while PkLocalFiniteElementMap only works for simplex)
    //VARIATIONPOINT(FEM; using FEM; Dune::PDELab::QkLocalFiniteElementMap<GV, GV::ctype, Real, degree>;)
    using FEM = Dune::PDELab::QkLocalFiniteElementMap<GV, GV::ctype, Real, degree>;
    //using FEM = Dune::PDELab::PkLocalFiniteElementMap<GV, GV::ctype, Real, degree>;
    //using FEM = Dune::PDELab::QkDGLocalFiniteElementMap<GV::ctype, Real, degree, dim>;
    //using FEM = Dune::PDELab::OPBLocalFiniteElementMap<GV::ctype, Real, degree, dim, basicElementType, Dune::GMPField<512>, Dune::PB::Qk>;
    //using FEM = Dune::PDELab::OPBLocalFiniteElementMap<GV::ctype, Real, degree, dim, basicElementType, Dune::GMPField<512>, Dune::PB::Pk>;

    FEM fem = Dune::SPL::FEMFactory<FEM>::construct(gv);

    // vector backend
    using VBE = Dune::PDELab::ISTLVectorBackend<>;

    // ##### variation point #####
    // ### select type of Dirichlet condition.
    //VARIATIONPOINT(CON; using CON; Dune::PDELab::ConformingDirichletConstraints;)
    using CON = Dune::PDELab::ConformingDirichletConstraints;
    //using CON = Dune::PDELab::NoConstraints;

    // construct grid function space
    using GFS = Dune::PDELab::GridFunctionSpace<GV, FEM, CON, VBE>;
    GFS gfs(gv, fem);

    // make constraints container
    using CC = typename GFS::template ConstraintsContainer<Real>::Type;
    CC cc;
    Dune::PDELab::ConvectionDiffusionBoundaryConditionAdapter<Problem> bctype(gv, problem);

    // make local operator
    const Dune::PDELab::ConvectionDiffusionDGMethod::Type dgMethod
        = Dune::PDELab::ConvectionDiffusionDGMethod::SIPG;
    const Dune::PDELab::ConvectionDiffusionDGWeights::Type dgWeights
        = Dune::PDELab::ConvectionDiffusionDGWeights::weightsOn;
    const Real dgPenalty = 4;
    using LOP = Dune::PDELab::ConvectionDiffusionDG<Problem, FEM>;
    LOP lop(problem, dgMethod, dgWeights, dgPenalty);
    using MBE = Dune::PDELab::ISTLMatrixBackend;
    MBE mbe;
    using GO = Dune::PDELab::GridOperator<GFS, GFS, LOP, MBE, Real, Real, Real, CC, CC>;
    GO go(gfs, cc, gfs, cc, lop, mbe);

    // make a vector of degree of freedom vectors and initialize it with Dirichlet extension.
    // G represents the boundary condition and the analytic solution
    using U = typename GO::Traits::Domain;
    U u(gfs, 0.0);
    using G = Dune::PDELab::ConvectionDiffusionDirichletExtensionAdapter<Problem>;
    G g(gv, problem);
    Dune::PDELab::interpolate(g, gfs, u);

    // initialize constraints container
    Dune::PDELab::constraints(bctype, gfs, cc);
    Dune::PDELab::set_nonconstrained_dofs(cc, 0.0, u);

    // ##### variation point #####
    // ### select linear solver and preconditioner
    //VARIATIONPOINT(LS; using LS; Dune::PDELab::ISTLBackend_SEQ_CG_ILU0;)
    using LS = Dune::PDELab::ISTLBackend_SEQ_CG_ILU0;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_SUPERLU;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_LOOP_Jac;    
    //using LS = Dune::PDELab::ISTLBackend_SEQ_CG_Jac;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_CG_SSOR;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_CG_AMG_SSOR;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_BCGS_Jac;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_BCGS_SSOR;
    //using LS = Dune::PDELab::ISTLBackend_SEQ_MINRES_SSOR;
    LS ls(10000, 2);

    // ##### variation point #####
    // ### select solver
    //VARIATIONPOINT(SLP; using SLP; Dune::PDELab::StationaryLinearProblemSolver<GO, LS, U>;)
    using SLP = Dune::PDELab::StationaryLinearProblemSolver<GO, LS, U>;
    //using SLP = Dune::PDELab::Newton<GO, LS, U, U>;

    // solve problem
    SLP slp = Dune::SPL::SolverFactory<SLP>::construct(go, ls, u, 1e-12);
    slp.apply();

    // construct a grid function from coefficient vector
    using UDGF = Dune::PDELab::DiscreteGridFunction<GFS, U>;
    UDGF udgf(gfs, u);
    // store output as vtk
    Dune::SubsamplingVTKWriter<GV> vtkwriter(gv, degree - 1);
    vtkwriter.addVertexData(new Dune::PDELab::VTKGridFunctionAdapter<UDGF>(udgf, "u_h"));
    vtkwriter.addVertexData(new Dune::PDELab::VTKGridFunctionAdapter<G>(g, "u"));
    vtkwriter.write("diffusion", Dune::VTK::appendedraw);
  } catch (Dune::Exception& ex) {
    std::cerr << ex.what() << "\n";
    return -1;
  } catch (std::exception& ex) {
    std::cerr << ex.what() << "\n";
    return -1;
  }
  return 0;
}
