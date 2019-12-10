
add_executable("diffusion" diffusion.cc)
target_link_dune_default_libraries("diffusion")
add_dune_gmp_flags("diffusion")
add_dune_ug_flags("diffusion")

