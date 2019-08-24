import { ApiInteractor } from '@/core/api-interactor';
import { CommentMapper } from './comment-mapper';
import { UserMapper } from '@/user/common/user-mapper';

/**
 * Interactor for working with the comment portion of the API.
 */
export abstract class CommentApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild comment entities.
     */
    protected commentMapper: CommentMapper;

    /**
     * Create a new comment api interactor.
     */
    constructor() {
        super();
        this.commentMapper = new CommentMapper(new UserMapper());
    }
}