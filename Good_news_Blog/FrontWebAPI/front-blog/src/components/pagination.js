import React from "react";
import { MDBPagination, 
    MDBPageItem, 
    MDBPageNav } from "mdbreact";

    class PaginationComponent extends React.Component {

        render() {
            return(
                <MDBPagination className="justify-content-center" size='lg' color="teal">
                    <MDBPageItem disabled>
                        <MDBPageNav aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                            <span className="sr-only">Previous</span>
                     </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem active>
                        <MDBPageNav>
                            1 <span className="sr-only">(current)</span>
                        </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem>
                        <MDBPageNav>
                            2
                        </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem>
                        <MDBPageNav>
                            3
                        </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem>
                        <MDBPageNav>
                            4
                        </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem>
                        <MDBPageNav>
                            5
                        </MDBPageNav>
                    </MDBPageItem>
                    <MDBPageItem>
                    <MDBPageNav>
                        &raquo;
                    </MDBPageNav>
                    </MDBPageItem>
                </MDBPagination>
            )
        }
    }

    export default PaginationComponent;