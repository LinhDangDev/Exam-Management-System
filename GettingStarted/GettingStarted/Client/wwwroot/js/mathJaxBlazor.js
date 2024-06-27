var MathJax = window.MathJax;
var promise = new Promise(function (resolved, rej) { resolved(); });
export function applySettings(texSettings) {
    MathJax.config.tex.inlineMath = texSettings.inlineMath;
    MathJax.config.tex.displayMath = texSettings.displayMath;
    //MathJax.startup.input[0].findTeX.options.processEscapes = texSettings.processEscapes;
    //MathJax.startup.input[0].findTeX.options.processEnvironments = texSettings.processEnvironments;
    //MathJax.startup.input[0].findTeX.options.processRefs = texSettings.processRefs;
    MathJax.startup.getComponents();
}
export function typesetPromise() {
    promise = promise.then(function () {
        typesetClear();
        return MathJax.typesetPromise();
    }).catch(function (err) {
        console.log(err);
    });
}
export function typesetClear() {
    try {
        undoTypeset();
        //MathJax.startup.document.state(0);
        MathJax.texReset();
        MathJax.typesetClear();
        MathJax.startup.document.clear();
    }
    catch (ex) {
        console.log(ex);
    }
}
export function undoTypeset() {
    var list = MathJax.startup.document.getMathItemsWithin(document.body);
    //var list = MathJax.startup.document.math.toArray();  // does not work anymore.
    for (var i = 0; i < list.length; i++) {
        list[i].start.node.outerHTML = list[i].start.delim + list[i].math + list[i].end.delim;
    }
}
export function processLatex(input, isDisplay) {
    return MathJax.tex2chtmlPromise(input, { display: isDisplay }).then(function (node) {
        //
        //  The promise returns the typeset node, which we add to the output
        //  Then update the document to include the adjusted CSS for the
        //    content of the new equation.
        //
        MathJax.startup.document.clear();
        MathJax.startup.document.updateDocument();
        return node.outerHTML;
    }).catch(function (err) {
        return err.message;
    });
}
export function processMathML(input) {
    return MathJax.mathml2chtmlPromise(input).then(function (node) {
        //
        //  The promise returns the typeset node, which we add to the output
        //  Then update the document to include the adjusted CSS for the
        //    content of the new equation.
        //
        MathJax.startup.document.clear();
        MathJax.startup.document.updateDocument();
        return node.outerHTML;
    }).catch(function (err) {
        return err.message;
    });
}
//# sourceMappingURL=mathJaxBlazor.js.map