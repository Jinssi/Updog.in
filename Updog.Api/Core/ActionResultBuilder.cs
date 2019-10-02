using System;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Application.Validation;

namespace Updog.Api {
    /// <summary>
    /// Builder that generates an ActionResult to return from the controller endpoint.
    /// Implements IOutputPort so it can be passed into application layer.
    /// </summary>
    public sealed class ActionResultBuilder : IOutputPort {
        #region Fields
        /// <summary>
        /// The result that was built.
        /// </summary>
        private ActionResult? result;
        #endregion

        #region Publics
        /// <summary>
        /// Build a 200 OK result that has a body.
        /// </summary>
        /// <param name="output">The payload.</param>
        /// <typeparam name="TResult">The type of payload.</typeparam>
        public void Success<TResult>(TResult? output = null) where TResult : class {
            var jResult = new JsonResult(output);
            jResult.StatusCode = 200;

            result = jResult;
        }

        /// <summary>
        /// Build a 404 Not Found result.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void NotFound(string? message = null) {
            result = new NotFoundObjectResult(message);
        }

        /// <summary>
        /// Build a 400 Bad Request result.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void BadInput(string? message = null) {
            result = new BadRequestObjectResult(message);
        }

        /// <summary>
        /// Build a 401 Unauthorized result.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void Unauthorized(string? message = null) {
            result = new UnauthorizedObjectResult(message);
        }

        /// <summary>
        /// Get the ActionResult built by the builder.
        /// </summary>
        /// <returns>The ActionResult that was built.</returns>
        public ActionResult Build() {
            if (result == null) {
                throw new InvalidOperationException("Result was never built");
            }

            var cachedResult = result;

            // Clear out the result so we don't dupe it accidentally.
            result = null;

            return cachedResult;
        }
        #endregion
    }
}