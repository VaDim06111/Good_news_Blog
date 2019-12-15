import React from 'react';

import { MDBMedia } from 'mdbreact';

class CommentBlock extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            comments: props.comments
        }
    }

    render() {
        try {
            const {comments} = this.state;
           
            return(
                <div>
                {comments.map(comment => (
                        <MDBMedia key={comment.id} className="d-block d-md-flex mt-4">
                          <img className="card-img-64 d-flex mx-auto mb-3" src="https://mdbootstrap.com/img/Photos/Avatars/img (20).jpg" alt="" />
                          <MDBMedia body className="text-center text-md-left ml-md-3 ml-0" style={{fontSize: '1.3rem'}}>
                            <h3 className="font-weight-bold mt-0">{comment.author.userName}</h3>
                            {comment.text}
                          </MDBMedia>
                        </MDBMedia>
                ))}  
                </div>                   
            )
        }
        catch(ex) {
            return <div></div>
        }         
    }
}

export default CommentBlock;