import React from "react";
import { MDBPagination, 
    MDBPageItem, 
    MDBPageNav } from "mdbreact";

    class PaginationComponent extends React.Component {

        constructor(props) {
            super(props);
            this.state = {
              page: props.page,
              total: props.total
            };
          }
 
        render() {
            const { page, total } = this.state;

            let RenderPages = () => {
                const pageNumbers = [];
                for (let i = (page - 2) > 0 ? page - 2 : (page - 1) > 0 ? page - 1 : page; i <= ((page + 2) < total ? page + 2 : (page + 1) < total ? page + 1 : page); i++) {
                    pageNumbers.push(i);
                }
            
                return(
                    pageNumbers.map(number => {
                        let classes = page === number ? true : false;
                      
                        return (
                          <MDBPageItem key={number} active={classes} onClick={() => this.props.updateData(number)}>
                                <MDBPageNav >
                                    {number}
                                </MDBPageNav>
                            </MDBPageItem>
                        );
                      }));
                }

            return(
                <MDBPagination className="justify-content-center" size='lg' color="teal">
                    <MDBPageItem disabled>
                        <MDBPageNav onClick={() => this.props.updateData(page - 1)} aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                            <span className="sr-only">Previous</span>
                     </MDBPageNav>
                    </MDBPageItem>
                    <RenderPages />
                    <MDBPageItem>
                    <MDBPageNav onClick={() => this.props.updateData(page + 1)}>
                        &raquo;
                    </MDBPageNav>
                    </MDBPageItem>
                </MDBPagination>
            )
        }
    }

    export default PaginationComponent;