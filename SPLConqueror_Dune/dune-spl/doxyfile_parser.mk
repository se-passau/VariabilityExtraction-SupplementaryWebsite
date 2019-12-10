$(variable) =

include $(DOXYFILE)

readlink:
	@for i in $($(variable)); do \
	  case "$$i" in \
	  *mainpage) ;; \
	  *mainpage.txt) ;; \
	  @*) \
        echo "`pwd`$$i" | sed -e 's/@srcdir@//' -e 's/@top_srcdir@/\/..\/../'; \
	  ;; \
	  *) \
	    if test -e "$$i"; then \
	      readlink -f "$$i"; \
	    fi; \
	  ;; \
	  esac \
	done

print:
	@echo '$($(variable))'
