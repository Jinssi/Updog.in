import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { User } from '../domain/user';
import { UserCredentials } from '../domain/user-credentials';
import { UserLoginInteractor } from '../interactors/login/user-login-interactor';
import { UserLogin } from '../domain/user-login';
import { UserRegisterInteractor } from '../interactors/register/user-register-interactor';
import { UserFinderByUsername } from '../interactors/find-by-username/user-finder-by-username';
import { UserRegistration } from '../domain/user-registration';
import { UserMutation } from './user-mutation';
import { StoreNamespace } from '@/core/store/store-namespace';
import { UserUsernameAvailableChecker } from '../interactors/is-username-available/user-username-available-checker';
import { UserReLoginInteractor } from '../interactors/relogin/user-relogin-interactor';
import { UserAction } from './user-action';
import { StoreUtils } from '@/core/store/store-utils';
import { SpaceAction } from '@/space/store/space-action';
import { PostMutation } from '@/post/store/post-mutation';
import { CommentMutation } from '@/comment/store/comment-mutation';
import { SpaceMutation } from '@/space/store/space-mutation';

/**
 * Vuex store module for managing user data.
 */
@Module({ namespaced: true, name: StoreNamespace.User })
export default class UserStore extends VuexModule {
    /**
     * The actively logged in user.
     */
    public userLogin: UserLogin | null = null;

    private findSubscribedSpacesAction = StoreUtils.buildAction(StoreNamespace.Space, SpaceAction.FindSubscribedSpaces);
    private findDefaultSpacesAction = StoreUtils.buildAction(StoreNamespace.Space, SpaceAction.FindDefaultSpaces);
    private clearPostVotesMutation = StoreUtils.buildMutation(StoreNamespace.Post, PostMutation.ClearVotes);
    private clearCommentVotesMutation = StoreUtils.buildMutation(StoreNamespace.Comment, CommentMutation.ClearVotes);
    private clearSubscribedSpacesMutation = StoreUtils.buildMutation(
        StoreNamespace.Space,
        SpaceMutation.ClearSubscribed
    );

    /**
     * Get the auth token for the currently logged in user.
     */
    get authToken() {
        if (this.userLogin == null) {
            return '';
        }

        return this.userLogin.authToken;
    }

    /**
     * Set a log in in the store module.
     * @param login The login to set.
     */
    @Mutation
    public [UserMutation.SetLogin](login: UserLogin) {
        this.userLogin = login;
    }

    /**
     * Clear the active login.
     */
    @Mutation
    public [UserMutation.ClearLogin]() {
        this.userLogin = null;
    }

    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    @Action({ rawError: true })
    public async [UserAction.FindByUsername](username: string) {
        return new UserFinderByUsername().handle(username);
    }

    /**
     * Check to see if a username is available.
     * @param username The username to check.
     */
    @Action({ rawError: true })
    public async [UserAction.IsUsernameAvailable](username: string) {
        return new UserUsernameAvailableChecker().handle(username);
    }

    /**
     * Log in an existing user with the backend.
     * @param userCreds The username and password to log in with.
     */
    @Action({ rawError: true })
    public async [UserAction.Login](userCreds: UserCredentials) {
        const login = await new UserLoginInteractor().handle(userCreds);
        this.context.commit(UserMutation.SetLogin, login);
        this.context.dispatch(this.findSubscribedSpacesAction, {}, { root: true });

        return login;
    }

    /**
     * Re log in a user using an older auth token.
     * @param authToken The auth token to authenticate.
     */
    @Action({ rawError: true })
    public async [UserAction.Relogin](authToken: string) {
        const login = await new UserReLoginInteractor().handle(authToken);
        this.context.commit(UserMutation.SetLogin, login);
        this.context.dispatch(this.findDefaultSpacesAction, {}, { root: true });

        return login;
    }

    /**
     * Log out the active user.
     */
    @Action({ rawError: true })
    public async [UserAction.Logout]() {
        this.context.commit(UserMutation.ClearLogin);
        this.context.commit(this.clearPostVotesMutation, {}, { root: true });
        this.context.commit(this.clearCommentVotesMutation, {}, { root: true });
        this.context.commit(this.clearSubscribedSpacesMutation, {}, { root: true });
    }

    /**
     * Register a new user with the backend.
     * @param userReg The user registration data
     */
    @Action({ rawError: true })
    public async [UserAction.Register](userReg: UserRegistration) {
        const login = await new UserRegisterInteractor().handle(userReg);
        this.context.commit(UserMutation.SetLogin, login);
        this.context.dispatch(this.findSubscribedSpacesAction, {}, { root: true });

        return login;
    }
}
