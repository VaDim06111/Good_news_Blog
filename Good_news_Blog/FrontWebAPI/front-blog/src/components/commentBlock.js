import React from 'react';
import TimeAgo from 'react-timeago'
import russianStrings from 'react-timeago/lib/language-strings/ru'
import buildFormatter from 'react-timeago/lib/formatters/buildFormatter'

import { MDBMedia } from 'mdbreact';

class CommentBlock extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            comments: props.comments
        }
    }

    componentDidUpdate(prevProps) {
        if(this.props !== prevProps) {
            this.setState({
                comments: this.props.comments
          })
        }
    }

    
    render() {
        try {
            const {comments} = this.state;
            const formatter = buildFormatter(russianStrings)

            return(
                <div>
                {comments.map(comment => (
                        <MDBMedia key={comment.id} className="d-block d-md-flex mt-4">
                          <img className="card-img-64 d-flex mx-auto mb-3" src="https://mdbootstrap.com/img/Photos/Avatars/img (20).jpg" alt="" />
                          <MDBMedia body className="text-center text-md-left ml-md-3 ml-0" style={{fontSize: '1.8rem'}}>
                            <h2 className="font-weight-bold mt-0">{comment.author.userName}</h2>
                            <h6><TimeAgo date={comment.pubDateTime + '+00:00'} formatter={formatter} /></h6>
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